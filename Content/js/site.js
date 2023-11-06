function showMessage(msg, messageType, nextURL, timer) {
    if (timer == undefined || timer == 0) {
        timer = 5000
    }
    document.querySelector(".alert > #txtError").innerHTML = msg
    if (messageType == "warning") {
        document.querySelector(".alert").style.background = "#f44336"
    }
    if (messageType == "info") {
        document.querySelector(".alert").style.background = "#9dd99e"
    }
    document.querySelector(".alert").style.display = "block"
    setTimeout(function () {
        document.querySelector(".alert").style.display = "none"
        if (nextURL == undefined || nextURL == "") {
            return
        }
        location.href = nextURL
    }, timer)
}

async function getResponse(url, type, data, nextURL, timer) {
    const response = await fetch(url, {
        method: type,
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify(data),
    })
    const json = await response.json()
    console.log(json)
    if (json.ResultCode == 200) {
        if (nextURL == "" || json.Message == "") {
            return json.ResultString
        }
        showMessage(json.Message, "info", nextURL, timer)
        return json.ResultString
    }
    showMessage(json.Message, "warning")
    if (nextURL == undefined || nextURL == "") {
        return
    }
}