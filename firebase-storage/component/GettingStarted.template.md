Get Started with Firebase Storage for Android

Firebase Storage is built for app developers who need to store and serve user-generated content, such as photos or videos.

{FIREBASE-CONFIGURE}


### Create a singleton instance

The first step in accessing your storage bucket is to create an instance of FirebaseStorage:

```csharp
var storage = FirebaseStorage.Instance;
```

Your files are stored in a Firebase Storage bucket. The files in this bucket are presented in a hierarchical structure, just like the file system on your local hard disk. By creating a reference to a file, your app gains access to it. These references can then be used to upload or download data, get or update metadata or delete the file. A reference can either point to a specific file or to a higher level in the hierarchy.



### Create a Reference

Create a reference to upload, download, or delete a file, or to get or update its metadata. A reference can be thought of as a pointer to a file in the cloud. References are lightweight, so you can create as many as you need. They are also reusable for multiple operations.

References are created using the `FirebaseStorage` singleton instance provided by the SDK by calling its `GetReferenceFromUrl` method and passing in a URL of the form `gs://<your-firebase-storage-bucket>`. You can find this URL the Storage section of the Firebase console.

```csharp
// Create a storage reference from our app
var storageRef = storage.GetReferenceFromUrl ("gs://<your-bucket-name>");
```

You can create a reference to a location lower in the tree, say `images/space.jpg` by using the `GetChild ()` method on an existing reference.

```csharp
// Create a child reference
// imagesRef now points to "images"
var imagesRef = storageRef.Child ("images");

// Child references can also take paths
// spaceRef now points to "users/me/profile.png
// imagesRef still points to "images"
var spaceRef = storageRef.Child ("images/space.jpg");
```


### Navigate with References

You can also use the `Parent` and `Root properties to navigate up in our file hierarchy. `Parent` navigates up one level, while `Root` navigates all the way to the top.

```csharp
// Parent allows us to move our reference to point to
// imagesRef now points to 'images'
imagesRef = spaceRef.Parent;

// Root allows us to move all the way back to the top of our bucket
// rootRef now points to the root
var rootRef = spaceRef.Root;
```

`Child()`, `Parent`, and `Root` can be chained together multiple times, as each returns a reference. But calling `Root.Parent` returns null.

```csharp
// References can be chained together multiple times
// earthRef points to 'images/earth.jpg'
var earthRef = spaceRef.Parent.Child ("earth.jpg");

// nullRef is null, since the parent of root is null
var nullRef = spaceRef.Root.Parent;
```



### Reference Properties

You can inspect references to better understand the files they point to using the `Path, `Name`, and `Bucket` properties. These properties get the file's full path, name and bucket.

```csharp
// Reference's path is: "images/space.jpg"
// This is analogous to a file path on disk
spaceRef.Path;

// Reference's name is the last segment of the full path: "space.jpg"
// This is analogous to the file name
spaceRef.Name;

// Reference's bucket is the name of the storage bucket that the files are stored in
spaceRef.Bucket;
```


### Limitations on References

Reference paths and names can contain any sequence of valid Unicode characters, but certain restrictions are imposed including:

Total length of reference.FullPath must be between 1 and 1024 bytes when UTF-8 encoded.

No Carriage Return or Line Feed characters.
Avoid using #, [, ], *, or ?, as these do not work well with other tools.


{FIREBASE-SAMPLES}



{FIREBASE-LEARNMORE}



{LINKS}
