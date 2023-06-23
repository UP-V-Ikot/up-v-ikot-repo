# CS 199: UP V-Ikot
Official repository for UP V-Ikot's CS 199 Project and Sprint development.

### Installation Guide
1. Install [Unity](https://unity.com/download) (NOTE: This project was made and tested using Unity Version 2020.3.40f1).

![image](https://github.com/UP-V-Ikot/up-v-ikot-repo/assets/99495442/570f52c5-931c-4d32-ba3d-469893c9829e)

2. Clone this repository.
3. Install the [Vuforia SDK package](https://library.vuforia.com/getting-started/getting-started-vuforia-engine-unity) and follow the steps to activate Vuforia for Unity.

![image](https://github.com/UP-V-Ikot/up-v-ikot-repo/assets/99495442/b2d2a3c5-ab0c-4e56-a461-21ebefc26367)

4. Download the 3D models and place the `.fbx` and its associated materials folder into the corresponding `Models` subdirectory (either `Buildings-exterior` or `Buildings-interior`) depending on the type of 3D model.
5. In the editor, click on the `.fbx` file and in the inspector window, set `Location` to `Use External Materials (Legacy)`. Click on `Apply` and the proper textures should apply to the 3D models.

![image](https://github.com/UP-V-Ikot/up-v-ikot-repo/assets/99495442/c49a2356-2853-4280-963e-8fc13d4bea50)

### How to Build APK
1. Under `File > Build Settings > Player Settings > Other Settings`, make sure that these options are followed to ensure proper testing and smooth integration with Vuforia.

![image](https://github.com/UP-V-Ikot/up-v-ikot-repo/assets/99495442/1505053f-1170-4a25-87bc-5bfd6da86b9d)

2. Go back to `Build Settings` and click on `Build` to your desired directory.

### How to Test 
1. To test the GPS feature, the APK must be built and installed on any supported Android device. Follow the instructions above.
2. Download any location spoofer application (NOTE: The group used [Fake GPS Location Spoofer](https://play.google.com/store/apps/details?id=com.incorporateapps.fakegps.fre&hl=en&gl=US)) to simulate any desired GPS coordinates. 
3. Once installed on mobile, follow the necessary testing procedures which in the tester guide which can be found in the [tester kit](https://tinyurl.com/UPV-IkotTestKitv2).

### Minimum Specifications
1. Any Android device with a decent camera running `Android 9.0+`.
2. A PC capable of handling Unity and high-resolution 3D models.
