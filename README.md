# SystemVolumePlugin-for-Unity
A set of tools for Unity to allow handling system volume for Android and iOS.

# Install
SystemVolumePlugin-for-Unity.unitypackage

# Usage
```cs
using SystemVolume;
```

#### Example: Get/Set
```cs
public void Example()
{
  var controller = new SystemVolumeController();
  controller.Volume = 0.5f;
  Debug.Log(controller.Volume);
}
```

#### Example: Callback when the volume buttons is pressed
```cs
public void Example()
{
  var controller = new SystemVolumeController();
  controller.OnChangeVolume = volume => {
    Debug.Log(volume);
  };
}
```
