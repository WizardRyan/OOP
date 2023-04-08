
function setDefaultColorPicker(el) {
    el.value = "#483D8B";
};

function showAlert(message) {
    alert(message);
}

var link = document.createElement('a');
link.download = `SavedDrawing.json`;

function downloadJSONFile(json_string) {
    var blob = new Blob([json_string], { type: 'text/plain' });
    link.href = window.URL.createObjectURL(blob);
    link.click();
}


//var exportImgLink = document.createElement('a');
//link.download = 'ExportedPicture.png';
//const saveBlob = (function() {
//  const a = document.createElement('a');
//  document.body.appendChild(a);
//  a.style.display = 'none';
//  return function saveData(blob, fileName) {
//     const url = window.URL.createObjectURL(blob);
//     a.href = url;
//     a.download = fileName;
//     a.click();
//  };
//}());

//function exportPicture() {
//    let canvas = document.getElementsByTagName("canvas")[0];
//    //let data = canvas.toDataURL("image/png");
//    //window.open(data);
//    canvas.toBlob((blob) => {
//        saveBlob(blob, `screencapture-${canvas.width}x${canvas.height}.png`);
//    });

//    //window.location = data;
//    //const res = await fetch(data);
//    //const blob = await res.blob();
//    //exportImgLink.href = window.URL.createObjectURL(blob);
//    //exportImgLink.href = data;
//    //exportImgLink.click();
//}

//function hide(el) {
//    el.style.display = "none";
//}

//function show(el) {
//    el.style.display = "block";
//}

//function saveAs(uri, filename) {
//    var link = document.createElement('a');
//    if (typeof link.download === 'string') {
//        link.href = uri;
//        link.download = filename;

//        //Firefox requires the link to be in the body
//        document.body.appendChild(link);

//        //simulate click
//        link.click();

//        //remove the link when done
//        document.body.removeChild(link);
//    } else {
//        window.open(uri);
//    }
//}