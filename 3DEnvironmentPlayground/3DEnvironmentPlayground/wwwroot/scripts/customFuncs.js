
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