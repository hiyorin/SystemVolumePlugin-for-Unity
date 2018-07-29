package com.hiyorin.systemvolumeplugin;

import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.Intent;

import android.media.AudioManager;

import com.unity3d.player.UnityPlayer;

public class SystemVolumeReceiver extends BroadcastReceiver {
    @Override
    public void onReceive(Context context, Intent intent) {
        if(intent.getAction().equals("android.media.VOLUME_CHANGED_ACTION")) {
            AudioManager am = (AudioManager)context.getSystemService(Context.AUDIO_SERVICE);
            float currentVolume = am.getStreamVolume(AudioManager.STREAM_MUSIC);
            float maxVolume = am.getStreamMaxVolume(AudioManager.STREAM_MUSIC);
            float volume = currentVolume / maxVolume;
            UnityPlayer.UnitySendMessage("SystemVolumePlugin","OnChangeSystemVolume", String.valueOf(volume));
        }
    }
}
