#import <Photos/Photos.h>

void _AddImageToGallery(const char* file)
{
     NSLog(@"Adding image to gallery.");
		
    // Create a NSString
    NSString *path = [NSString stringWithUTF8String:file];
    
    // Check that the file exists
    if( ![[NSFileManager defaultManager] fileExistsAtPath:path]) {
        NSLog(@"Can not add image to camera roll, the path is invalid.");
        return;
    }
    
    // Create an UIImage from the file
    UIImage *image = [UIImage imageNamed:path];
    if( image  == NULL) {
        NSLog(@"Can not add image to camera roll, failed to create the UIImage.");
        return;
    }
    
    // Add the image to the camera roll
    UIImageWriteToSavedPhotosAlbum( image, nil, NULL, NULL );
}


bool _HasGalleryAuthorization()
{
	return ([PHPhotoLibrary authorizationStatus] == PHAuthorizationStatusAuthorized);
}

void _RequestGalleryAuthorization()
{
	NSLog(@"Request gallery authorization");
     [PHPhotoLibrary requestAuthorization:^(PHAuthorizationStatus status) {
		NSLog(@"Gallery authorization: %ld", (long)status);
     }]; 
}