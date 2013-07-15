//http://kroltech.com/2013/03/building-a-web-app-using-backbone-js-and-require-js-part-1/

(function () {
    var root = this;
    require.config({
        baseUrl: "/scripts/app/",
        paths: {
            "underscore" : "../underscore.min",
            "backbone"   : "../backbone.min",
            "moment"     : "../moment.min"
          //"jquery"     : "../jquery-1.9.1.min"   
        },
        shim: {
            "underscore": {
                exports: '_'
            },
            "backbone"   : {
                deps     : ['underscore'/*, 'jquery'*/],
                exports  : 'Backbone'
            },
            'jquery.masonry'      : { deps: ['jquery'], exports: 'masonry' },
            'jquery.imagesLoaded' : { deps: ['jquery'], exports: 'imagesloaded' },
            'jquery.simplemodal'  : { deps: ['jquery'], exports: 'modal' }
        }
    });

    //these are defined externally so they can be used in our view templates:
    define('jquery', [], function () { return root.jQuery; });
    define('moment', [], function () { return root.moment; });

    //require(['app'], function (app) {
    //    app.run();
    //});
})();