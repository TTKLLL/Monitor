
function SuAlert(str) {
    layer.alert(str, { icon: 1 });
}

function errAlert(str) {
    layer.alert(str, { icon: 2 });
}

function infoAlert(str) {
    layer.alert(str, { icon: 0 });
}

function autoAlert(str) {
    layer.msg(str, { time: 1000 })
}
