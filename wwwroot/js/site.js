function showMessage(msg, messageType, nextURL, timer) {
    if (timer == undefined || timer == 0) {
        timer = 5000;
    }
    document.querySelector(".alert > #txtError").innerHTML = msg;
    if (messageType == "warning") {
        document.querySelector(".alert").style.background = "#f44336";
    }
    if (messageType == "info") {
        document.querySelector(".alert").style.background = "#9dd99e";
    }
    document.querySelector(".alert").style.display = "block";
    setTimeout(function () {
        document.querySelector(".alert").style.display = "none";
        if (nextURL == undefined || nextURL == "") {
            return;
        }
        location.href = nextURL;
    }, timer);
}

async function getResponse(url, type, data, nextURL, timer) {
    const response = await fetch(url, {
        method: type,
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify(data),
    });
    const json = await response.json();
    if (json.resultCode == 200) {
        showMessage(json.message, "info", nextURL, timer);
        return;
    }
    showMessage(json.message, "warning");
    if (nextURL == undefined || nextURL == "") {
        return;
    }
}