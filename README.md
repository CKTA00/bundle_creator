# bundle_creator
Companion project to Room Arranger VR application for creating asset bundles

### What is an asset bundle?

Asset bundle is a special file for custom models and textures that can be used in Unity applications. In case of Room Arranger VR, bundles need to be created with accordance to following rules in order for them to work properly in the application.

# How to create a bundle for RoomArranger?

### Preparation

In order to create a bundle you need to download Unity editor. The latest Long Support Version (LTS) is recommended. You can download the editor from official Unity website: https://unity.com/download

Download this repository and open it as an Unity project. Then you need to add assets to your Assets folder:
- models: .fbx or .blend
- textures for flat surfaces (floors, walls, ceiling): .png
Asset folder can be found in the Project window. If you don't see the Project card you can open it via Window>General menu at the top. Other windows like Scene, Hierarchy and Inspector also can be found here.

The next step is to collect your assets and create special objects depending on the type of asset you are creating, that will be understood by the Room Arranger VR.

### Creating custom furniture

1. Make sure that your Sample Scene contains only "Scene" object. Delete other objects, including models that you bundled previously.

![obraz](https://user-images.githubusercontent.com/72377791/213673193-67a18e24-b81f-4bdf-90ec-58e3f1ad94a7.png)

2. Move your custom model from assets to the scene (drag and drop).

3. Make sure that your model has all textures applied. If not, you can try to reapply textures using the editor by unpacking your model by selecting it in the Project window, then extracting materials in the Inspector view, creating new materials and reapplying them to the models.

![obraz](https://user-images.githubusercontent.com/72377791/213672672-fa767ec0-b28a-45e4-9131-f4904ff8c007.png)

4. Click on your model in the scene or select it in the Hierarchy. In the inspector change the position of your object to 0,0,0 and make sure that your furniture is correctly rotated. You also might need to scale it.

5. Next in the inspector menu press the "Add Component" button at the bottom of the component list (you might scroll down). Choose Colliders>Box Collider or search for collider. Do not select other shapes of colliders or 2D colliders! Resize the box collider so it is roughly matching the shape of your furniture. You can do it in Scene view by dragging green dots, that are enabled after pressing Edit Collider button in the Box Collider

![obraz](https://user-images.githubusercontent.com/72377791/213676055-88d03851-2328-408d-90ec-1040c111e6f5.png)

You can use the gizmo to change perspective to one parallel to the axis in order to be more precise with scaling your collider.

![obraz](https://user-images.githubusercontent.com/72377791/213676409-92a46949-097d-4273-b0d7-b0f19c63813e.png)

End result should look something like this:

![obraz](https://user-images.githubusercontent.com/72377791/213676481-5c1d4872-e8bd-4d7c-830f-fd8bd526793d.png)

You can also include other visual components like light, particle system, animation and also sound although they are not recommended. Do not add rigidbody or other functional components.

6. Rename your object to a fitting name. Then drag and drop your object from Hierarchy to Project (preferably separate directory than your raw files). You successfully created a prefab.

7. Choose the bundle name for this prefab (object you just moved to assets), by selecting it in the Project window. You can add new bundles or select existing ones. That name will become the category name in Room Arranger VR, so it is recommended to add multiple furniture to the same asset bundle name.

![obraz](https://user-images.githubusercontent.com/72377791/213677153-bc44cf1d-64a8-42b1-9972-89e6e74ccd82.png)

8. Press the play button at the top, and then press it again after a few seconds. Assuming that the name of the object has not been changed to something different than its prefab and there is only one object besides “Scene” in the Hierarchy, running the project will create a thumbnail for it and automatically link it to your asset bundle. You can skip this step or create your custom thumbnail.

![obraz](https://user-images.githubusercontent.com/72377791/213678368-197dbbb9-54dc-46d9-86f4-b868f5212bc1.png)

9. Delete object from scene. Now you can prepare another object. If you want to modify your object then drag and drop a prefab from assets you have just created.

### Creating custom flat texture (material)

1. In the Project menu, press the right mouse button and choose Create>Material. Give it a fitting name.

2. Select your material and in the inspector apply textures. You can apply just albedo texture or give it metallic, normal map, height map, occlusion textures, as well as modifying other options. You can apply your texture to a temporary object (GameObject>3D Object>...) or use build-in preview to see how your material looks. Change tiling if necessary if the texture is too dense/wide.

![obraz](https://user-images.githubusercontent.com/72377791/213680805-cd1f7fe8-a92c-41dd-961b-3c80504c14d1.png)

3. If you want to apply a smoothness map you need to add it as an alpha channel in a metallic map. Do not use alpha channel for albedo texture, because it is used for thumbnails. If you have a roughness map, also invert its colors so it becomes a smoothness map. You can do it in free image editor GIMP. Short tutorial in GIMP on adding smoothness maps will be coming soon...

4. Select the bundle name the same way as for furniture.

### Compiling bundles

After creating all furniture prefabs and flat surface materials you need to compile all bundles to the files. Choose the BundleCreator menu at the top and select Create Bundles in Room Arranger VR directory, to directly create bundles in the main application directory. Be aware that compiling bundles will delete all previous bundles in this directory, so if you want to add more bundles, you need to compile them to separate folder via "Create Bundles in Assets" and then move it to Furniture folder in C:\Users\username\AppData\LocalLow\PKozuch\RoomArrangerVR\ where username is your system username.

### Notice

Be careful to not import too large or too many assets at the same time because they can cause crashes or performance issues both in Unity editor and Room Arranger VR.
