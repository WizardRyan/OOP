function downloadStringAsFile(str, fName) {
    let blob = new Blob([str], { type: 'text/plain' });
    saveAs(blob, fName);
}