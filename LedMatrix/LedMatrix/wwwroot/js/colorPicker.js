
let cellColor = "rgb(255, 0, 0)";
const colorCellSize = 2;
const scaleDown = 8;
let colorPickerCanvas = document.getElementById("colorPicker");
let colorPickerContext = colorPickerCanvas.getContext("2d");

function fillColorPicker(e) {
    let blue = $('#blueSlider').val();
    for (let r = 0; r < 255; r+=scaleDown) {
        for (let g = 0; g < 255; g+=scaleDown) {
            colorPickerContext.fillStyle = `rgb(${r},${g},${blue}`;
            colorPickerContext.fillRect(r * colorCellSize, g * colorCellSize, colorCellSize * scaleDown, colorCellSize * scaleDown);
        }
    }
}

$(document).ready(function () {
    fillColorPicker();
    $('#blueSlider').on('input', fillColorPicker);
    $('#colorPicker').click(function (event) {
        let blue = $('#blueSlider').val();
        cellColor = `rgb(${Math.floor(event.offsetX / colorCellSize)}, ${Math.floor(event.offsetY / colorCellSize)}, ${blue})`
        $('#selectedColor').css('background-color', cellColor);
    });
});
