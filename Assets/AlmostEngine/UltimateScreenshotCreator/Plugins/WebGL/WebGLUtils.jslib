var ImageDownloaderPlugin = {

    _CanShare: function() {
        if (window.navigator.canShare) {
            return true;
        } else {
            return false;
        }
    },

    _ShareImage: function(data, filename, format) {


        var imageData = Pointer_stringify(data);
        var imageFileName = Pointer_stringify(filename);
        var contentType = 'image/' + Pointer_stringify(format);
        var fullFileName = Pointer_stringify(filename) + '.' + Pointer_stringify(format);

        // Converts the image data to binary
        // From http://stackoverflow.com/questions/14967647/
        // encode-decode-image-with-base64-breaks-image (2013-04-21)
        function fixBinary(data) {
            var length = data.length;
            var bytes = new ArrayBuffer(length);
            var arr = new Uint8Array(bytes);
            for (var i = 0; i < length; i++) {
                arr[i] = data.charCodeAt(i);
            }
            return bytes;
        }
        var bytes = fixBinary(atob(imageData));

        // Creates an image Blob
        var imageBlob = new Blob([bytes], { type: contentType });

        // Create files array containing the blob
        var file = new File([imageBlob], fullFileName, { type: contentType });
        var filesArray = [file];

        // Share when supported by browser
        if (window.navigator.canShare && window.navigator.canShare({ files: filesArray })) {
            window.navigator.share({
                files: filesArray,
                title: imageFileName,
                text: imageFileName,
            });
            console.log('Share successful.')
        } else {
            console.log('Share is not supported on this browser.')
        }
    },


    _ExportImage: function(data, filename, format) {


        var imageData = Pointer_stringify(data);
        var imageFileName = Pointer_stringify(filename);
        var contentType = 'image/' + Pointer_stringify(format);
        var fullFileName = Pointer_stringify(filename) + '.' + Pointer_stringify(format);

        // Converts the image data to binary
        // From http://stackoverflow.com/questions/14967647/
        // encode-decode-image-with-base64-breaks-image (2013-04-21)
        function fixBinary(data) {
            var length = data.length;
            var bytes = new ArrayBuffer(length);
            var arr = new Uint8Array(bytes);
            for (var i = 0; i < length; i++) {
                arr[i] = data.charCodeAt(i);
            }
            return bytes;
        }
        var bytes = fixBinary(atob(imageData));

        // Creates an image Blob
        var imageBlob = new Blob([bytes], { type: contentType });

        // Create files array containing the blob
        var file = new File([imageBlob], fullFileName, { type: contentType });
        var filesArray = [file];

        // For old browsers
        if (window.navigator.msSaveOrOpenBlob != null) {
            window.navigator.msSaveBlob(imageBlob, imageFileName);
            console.log('Bob saved using old browser method.')
        }
        // For recent browsers
        else {
            // Creates a clickable link that will download the image
            var link = document.createElement('a');
            link.download = imageFileName;
            link.innerHTML = 'DownloadFile';
            link.setAttribute('download', imageFileName);
            link.style.display = 'none';

            // Creates the click URL
            if (window.webkitURL != null) {
                link.href = window.webkitURL.createObjectURL(imageBlob);
            } else {
                link.href = window.URL.createObjectURL(imageBlob);
                document.body.appendChild(link);
            }

            //Calling the link click action
            link.click();

            // Clean
            if (window.webkitURL == null) {
                document.body.removeChild(link);
            }
            console.log('Bob saved using new browser method.')
        }
    }

};

mergeInto(LibraryManager.library, ImageDownloaderPlugin);