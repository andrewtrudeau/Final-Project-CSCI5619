# Assignment 4: Selection and Manipulation

**Due: Thursday, November 4, 10:00pm CDT**

In this assignment, you will implement several more advanced selection and manipulation techniques that were discussed in class.

## Submission Information

You should fill out this information before submitting your assignment.  Make sure to document the name and source of any third party assets such as 3D models, textures, or any other content used that was not solely written by you.  Include sufficient detail for the instructor or TA to easily find them, such as a download link.

Name: Andrew Trudeau 

UMN Email: trude135@umn.edu

Third Party Assets: N/A

Bonus Challenge Description (if applicable):

## Getting Started

Clone the assignment using GitHub Classroom.  The project has been configured for the Oculus Quest, and the [XR Interaction Toolkit](https://docs.unity3d.com/Packages/com.unity.xr.interaction.toolkit@1.0/manual/index.html) package has already been imported.  The project contains more sophisticated versions of the grabbing and pointing scripts we implemented in [Lecture 13](https://github.com/CSCI-5619-Fall-2021/Lecture-13).  Both controllers have the `GraspGrabber` and `PointGrabber` components, both of which extend from the `Grabber` base class.  Every object in the scene is `Grabbable` except for the ground.

## Rubric

Graded out of 20 points. 

1. Implement a **Fishing Reel** variant of the pointing technique (see [Lecture 14](https://www.beautiful.ai/player/-MmYjKi7-rNfp52MCqKE), slide 12).  When an object is grabbed using the laser pointer, the user should be able to translate the object forwards and backwards along the ray using the thumbstick.  This should work for both controllers.  (5)
   *Hint: the `PointGrabber` script creates an object called `Grab Point` and makes the grabbed object its child.  You can therefore manipulate the location of the object by moving the grab point.*
2. Implement the **Go-Go** grasping technique (see [Lecture 12](https://www.beautiful.ai/player/-Mm-BRr29z-cOImVB72w), slide 26) on both controllers.  This will involve computing the distance between each controller and the headset.  For distances less than some threshold *d*, the location of the controller should match the user's hand to allow them to easily grab nearby objects.  For distances greater than *d*, the hand should be scaled forward to extend the user's reach.  You should experiment with different values for *d* and the distance scale factor to settings that work comfortably. (4)
   *Hint: you should modify the `localPosition` transform of the `controller_l` and `controller_r` game objects, which are children of the `LeftHand Controller` and `RightHand Controller` game objects, respectively.*
3. The user should be able to turn the Go-Go technique on and off using the A button on the right controller or the X button on the left controller.  Pressing either button should toggle the Go-Go technique for *both* hands. (1)
4. Implement a **Spindle** between the two controllers (see [Lecture 14](https://www.beautiful.ai/player/-MmYjKi7-rNfp52MCqKE), slide 48).  You can use a small object such as a cube to mark the midpoint.  The spindle should only be active when the Go-Go technique is toggled off. (2)
5. When the user grabs an object with either hand when the spindle is active, the object should be translated to the midpoint of the spindle. (2)
6. When the user moves their hands, the grabbed object should be translated to follow the midpoint of the spindle.  (2)
7. When the user moves their hands, the grabbed object should be rotated to match the alignment of the spindle.  Note that you only need to worry about yaw and roll (rotation about the Y and Z axes).  (2)
8. When the user moves their hands closer together or further apart, the grabbed object should be scaled up or down based on the distance between them.  (2)

**Bonus Challenge:**  Implement any of the other manipulation techniques or come up with your own enhancements to one of the techniques above.  Creativity is encouraged!  However, it must be a spatial interaction.  This means it cannot be trivially implemented with a button press or joystick.  This bonus functionality should be added as a third mode that the user can toggle between using the A button the right controller or X button on the left controller.  Make sure to describe your bonus interaction at the submission information section of your readme file, so we know what to test when grading the assignment.  (2)

Make sure to document all third party assets in your readme file. ***Be aware that points will be deducted for using third party assets that are not properly documented.***

## Submission

You will need to check out and submit the project through GitHub classroom.  **Make sure your APK file is in the root folder.** Do not remove the `.gitignore` or `README.md` files.

Please test that your submission meets these requirements.  For example, after you check in your final version of the assignment to GitHub, check it out again to a new directory and make sure everything opens and runs correctly.  You can also test your APK file by installing it manually using [SideQuest](https://sidequestvr.com/).

## Acknowledgments

The scene uses assets from the [Playground Low Poly](https://assetstore.unity.com/packages/3d/environments/playground-low-poly-191533) package from the Unity Asset Store.

## License

Material for [CSCI 5619 Fall 2021](https://canvas.umn.edu/courses/268490) by [Evan Suma Rosenberg](https://illusioneering.umn.edu/) is licensed under a [Creative Commons Attribution-NonCommercial-ShareAlike 4.0 International License](http://creativecommons.org/licenses/by-nc-sa/4.0/).

The intent of choosing CC BY-NC-SA 4.0 is to allow individuals and instructors at non-profit entities to use this content.  This includes not-for-profit schools (K-12 and post-secondary). For-profit entities (or people creating courses for those sites) may not use this content without permission (this includes, but is not limited to, for-profit schools and universities and commercial education sites such as Coursera, Udacity, LinkedIn Learning, and other similar sites).   