define(['jquery'], function ($) {
    //获取url参数
    function getUrlParams(name) {
        var reg = new RegExp('(^|&)' + name + '=([^&]*)(&|$)', 'i');
        var r = window.location.search.substr(1).match(reg);
        if (r != null)
            return decodeURIComponent(r[2]);
        return null;
    };
    return {
        getUrlParams: getUrlParams,
    }
})