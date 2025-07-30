(function () {
    function addSearchBox() {
        var div = document.createElement("div");
        div.innerHTML = `<input type="text" id="search-endpoints" placeholder="Search endpoints..." 
                         style="margin-bottom: 10px; padding: 5px; width: 100%;">`;
        document.querySelector(".topbar-wrapper").after(div); // Added after the top bar

        document.getElementById("search-endpoints").addEventListener("input", function (e) {
            let searchValue = e.target.value.toLowerCase();
            document.querySelectorAll(".opblock-summary").forEach(function (el) {
                let text = el.textContent.toLowerCase();
                el.parentElement.style.display = text.includes(searchValue) ? "block" : "none";
            });
        });
    }

    window.onload = function () {
        setTimeout(addSearchBox, 500);  // Ensure it's added after Swagger UI is loaded
    };
})();
