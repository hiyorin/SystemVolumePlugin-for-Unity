#import <AVFoundation/AVFoundation.h>
#import <MediaPlayer/MediaPlayer.h>
#import "SystemVolumeReceiver.h"
#include "UnityInterface.h"

@implementation SystemVolumeReceiver

- (id)init
{
    [self startTrackingVolumeChanges];
    return [super init];
}

- (void)dealloc
{
    [self stopTrackingVolumeChanges];
}

#pragma mark - Start Tracking Volume Changes

- (void)startTrackingVolumeChanges
{
    [self addObserver];
    [self activateAudioSession];
}

- (void)addObserver
{
    [[NSNotificationCenter defaultCenter] addObserver:self selector:@selector(volumeChanged:) name:@"AVSystemController_SystemVolumeDidChangeNotification" object:nil];
}

- (void)activateAudioSession
{
    AVAudioSession *audioSession = [AVAudioSession sharedInstance];
    [audioSession setCategory:AVAudioSessionCategoryPlayback withOptions:AVAudioSessionCategoryOptionMixWithOthers error:nil];
    NSError *error;
    BOOL success = [audioSession setActive:YES error:&error];
    if (!success) {
        NSLog(@"Error activating audiosession: %@", error);
    }
}

#pragma mark - Observing Volume Changes

- (void)volumeChanged:(NSNotification *)notification
{
    NSString* volumeChangeType = notification.userInfo[@"AVSystemController_AudioVolumeChangeReasonNotificationParameter"];
    
    if ([volumeChangeType isEqualToString:@"ExplicitVolumeChange"]) {
        float volume = [notification.userInfo[@"AVSystemController_AudioVolumeNotificationParameter"] floatValue];
        NSString* volumeStr = [NSString stringWithFormat:@"%f", volume];
        UnitySendMessage("SystemVolumePlugin", "OnChangeSystemVolume", [volumeStr UTF8String]);
    }
}

#pragma mark - Stop Tracking Volume Changes

- (void)stopTrackingVolumeChanges
{
    [self removeObserver];
    [self deactivateAudioSession];
}

- (void)removeObserver
{
    [[NSNotificationCenter defaultCenter] removeObserver:self name:@"AVSystemController_SystemVolumeDidChangeNotification" object:nil];
}

- (void)deactivateAudioSession
{
    dispatch_async(dispatch_get_main_queue(), ^{
        NSError *error;
        BOOL success = [[AVAudioSession sharedInstance] setActive:NO error:&error];
        if (!success) {
            NSLog(@"Error deactivating audiosession: %@", error);
        }
    });
}

@end
