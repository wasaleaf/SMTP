window.reseizeService = {
    addResizeListenser: function (dotnetHelper) {
        window.onresize = function () {
            dotnetHelper.invokeMethodAsync('NotifyResize', window.innerWidth);
        };
    },
    removeResizelistener: function () {
        window.onresize = null;
    },
    getWindowWidth: function () {
        return window.innerWidth;
    }
};