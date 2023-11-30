// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(window).ready(() => {
  let availableTags = [];
  $("#startLocation").autocomplete({
    source: availableTags
  });


})

function SetToday() {
  var today = new Date().toISOString().slice(0, 10);
  document.getElementById('date').value = today;
}
function SetTomorrow() {
  var today = new Date(); 
  var tomorrow = new Date(today);
  tomorrow.setDate(today.getDate() + 1);
  document.getElementById('date').value = tomorrow.toISOString().slice(0, 10);

}


