// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

const BASE_URL = "https://localhost:7282";
let autoCompleteSetting = {
  source: async function (request, response) {
    try {
      const term = request.term;
      const data = await SearchBusLocation(term);

      const autocompleteData = data.map(item => ({
        label: item.value,
        value: item.key
      }));

      response(autocompleteData);
    } catch (error) {
      console.error('Error fetching bus locations:', error);
    }
  },
  minLength: 2,
  select: async function (event, ui) {
    console.log($(this), ui);
    const target = $(this);
    setTimeout(() => {
      target.val(ui.item.label);
      $(GetTargetId(target)).val(ui.item.value);
      SetHomePageStorageData();
    }, 1);

    return false;
  },
  focus: function (event, ui) {
    $(this).val(ui.item.label);
    return false;
  },
};
const startLocationSearch = $('#startLocationSearch');
const endLocationSearch = $('#endLocationSearch');
const locationTrans = $("#location-trans");
const suggestionSelectBtn = $(".suggestion-select");
const dateDomEl = $("#date");
const backButton = $("#back");
$(window).ready(() => {

  BuildDefaultData();
  startLocationSearch.autocomplete(autoCompleteSetting);
  endLocationSearch.autocomplete(autoCompleteSetting);
  locationTrans.click(() => LocationTransClickEvent());
  suggestionSelectBtn.click(function () { SuggestionSelectClickEvent($(this)); });
  dateDomEl.change(function () {
    var today = new Date().toISOString().split('T')[0];

    if (this.value < today) {
      alert('Seçilen tarih bugünden küçük olamaz!');
      this.value = today;
    }
    SetHomePageStorageData();
  })
  backButton.click(() => {
    window.location.href = "/";
  })

})
function BuildDefaultData() {
  const getHomePageStorageData = GetHomePageStorageData();
  if (getHomePageStorageData == null || getHomePageStorageData == undefined) {
    SetTomorrow();
    return;
  }
  SetLocations(getHomePageStorageData.startName, getHomePageStorageData.endName, getHomePageStorageData.startId, getHomePageStorageData.endId, true);
  const date = new Date(getHomePageStorageData.date);


  dateDomEl.val(date.toISOString().slice(0, 10));
  return;
}
function LocationTransClickEvent() {
  const startLocationSearchValue = startLocationSearch.val();
  const endLocationSearchValue = endLocationSearch.val();


  const startLocationHiddenInputValue = $(GetTargetId(startLocationSearch)).val();
  const endLocationHiddenInputValue = $(GetTargetId(endLocationSearch)).val();

  //trans

  SetLocations(endLocationSearchValue, startLocationSearchValue, endLocationHiddenInputValue, startLocationHiddenInputValue);


}
function GetTargetId(el) {
  return el.data("target-id");
}

function SetLocations(startName, endName, startId, endId, ignoreSetStorage = false) {
  startLocationSearch.val(startName);
  endLocationSearch.val(endName);
  $(GetTargetId(startLocationSearch)).val(startId);
  $(GetTargetId(endLocationSearch)).val(endId);
  if (ignoreSetStorage == false) {
    SetHomePageStorageData();
  }
}
const HOME_PAGE_STORAGE_KEY = "HOME_INDEX_STORAGE_KEY";
function SetHomePageStorageData() {
  const startId = $(GetTargetId(startLocationSearch)).val();
  const startName = startLocationSearch.val();
  const endId = $(GetTargetId(endLocationSearch)).val();
  const endName = endLocationSearch.val();
  const date = document.getElementById('date').value;
  localStorage.removeItem(HOME_PAGE_STORAGE_KEY);
  const data = {
    startId: startId,
    startName: startName,
    endId: endId,
    endName: endName,
    date: date
  };
  console.log(JSON.stringify(data));
  localStorage.setItem(HOME_PAGE_STORAGE_KEY, JSON.stringify(data));
}
function GetHomePageStorageData() {
  const getValue = localStorage.getItem(HOME_PAGE_STORAGE_KEY);
  return JSON.parse(getValue);
}
function SuggestionSelectClickEvent(el) {
  let startId = el.data("start-id");
  let endId = el.data("end-id");
  let startName = el.data("start-name");
  let endName = el.data("end-name");
  console.log(startId, endId, startName, endName);
  SetLocations(startName, endName, startId, endId);
}
async function SearchBusLocation(term) {
  try {

    const response = await fetch(`${BASE_URL}/Home/SearchBusLocationAsync/${term}`);

    if (!response.ok) {
      throw new Error('Network response was not ok');
    }

    const data = await response.json(); // Bekleyerek JSON verisini al

    // Gelen veri ile yapılabilecek işlemler
    console.log(data);

    return data; // Veriyi döndür
  } catch (error) {
    // Hata durumunda yapılacak işlemler
    console.error('There has been a problem with your fetch operation:', error);
    throw error; // Hata durumunu yeniden fırlat
  }
}
function SetToday() {
  var today = new Date().toISOString().slice(0, 10);
  document.getElementById('date').value = today;
  SetHomePageStorageData();
}
function SetTomorrow() {
  var today = new Date();
  var tomorrow = new Date(today);
  tomorrow.setDate(today.getDate() + 1);
  document.getElementById('date').value = tomorrow.toISOString().slice(0, 10);
  SetHomePageStorageData();

}


