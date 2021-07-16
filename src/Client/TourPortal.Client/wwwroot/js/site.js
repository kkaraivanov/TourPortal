export function addCss( path) {
    let newLink = document.createElement("link");
    newLink.setAttribute("class", "default-css");
    newLink.setAttribute("rel", "stylesheet");
    newLink.setAttribute("type", "text/css");
    newLink.setAttribute("href", `${path}`);

    let head = document.getElementsByTagName("head")[0];
    //head.appendChild(newLink);
    processAdd(head, newLink);
}

export function removeCss() {
    let links = document.querySelectorAll('head link.default-css');

    for (var i = 0; i < links.length; i++) {
        processRemove(links[i]);
    }
}

export function addScripts(path) {
    let body = document.getElementsByTagName('body')[0],
        script = document.createElement('script');

    script.setAttribute("class", "default-script");
    script.src = `${path}`;
    //body.appendChild(script);
    processAdd(body, script);
}

export function removeScripts() {
    let selector = document.querySelectorAll('body script.default-script');

    for (var i = 0; i < selector.length; i++) {
        processRemove(selector[i]);
    }
}

function processRemove(elmt) {
    if (elmt !== null && elmt !== '') {
        elmt.parentNode.removeChild(elmt);
    }
}

function processAdd(elmt, child) {
    elmt.appendChild(child);
}

window.getYear = function() {
    var currentDate = new Date();
    var currentYear = currentDate.getFullYear();

    return currentYear;
}