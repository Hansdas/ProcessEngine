
var app= new Vue({
    el:'#app',
    data:{
        menuHtml:''
    },
    mounted: function () {
        var that=this;
        layui.use('element', function () {
            var element = layui.element;

            //бн
        });
        //vueDom.totalCount=total;
    },
    methods:{
        menuOpen:function (url) {
            document.getElementById('iframeSrc').src=url;
        }
    }
})