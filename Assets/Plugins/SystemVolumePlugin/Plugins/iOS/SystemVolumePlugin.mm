#import <Foundation/Foundation.h>
#import <MediaPlayer/MediaPlayer.h>
#include "UnityInterface.h"
#import "SystemVolumeReceiver.h"

SystemVolumeReceiver* _receiver = nil;

extern "C" void _InitializeSystemVolume()
{
    _receiver = [[SystemVolumeReceiver alloc] init];
}

extern "C" void _DisposeSystemVolume()
{
    if (_receiver != nil) {
        [_receiver release];
        _receiver = nil;
    }
}

extern "C" void _SetSystemVolume(float volume)
{
    [[MPMusicPlayerController applicationMusicPlayer] setVolume:volume];
}

extern "C" float _GetSystemVolume()
{
    return [MPMusicPlayerController applicationMusicPlayer].volume;
}
