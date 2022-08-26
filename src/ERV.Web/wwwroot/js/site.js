var countryApp = countryApp || {
    Data:{
        Countries: null
    },
    Resources: {
        Language: document.cookie.indexOf("CurrentLanguage=ES") !== -1 ? "ES" : "EN",
        Get: new function (key) {

            let LangEs = function (key) {
                var obj = {
                    "_Home": "Inicio",
                };

                value = key in obj ? obj[key] : '';

                return value;
            };

            let LangEn = function (key) {
                var obj = {
                    "_Home": "Home",
                };

                value = key in obj ? obj[key] : '';

                return value;
            };


            return function (key) {
                var isSpanish = document.cookie.indexOf("CurrentLanguage=ES") !== -1;
                if (isSpanish) {
                    return ResourcesEs(key);
                }
                else {
                    return ResourcesEn(key);
                }
            };

        }
    },
    Constants: {
        BaseUrl: 'https://localhost:7031/'
    },
    Ajax: new function () {
        let isRequesting = false;
        let token = $("#__AjaxForm [name=__RequestVerificationToken]");
        let doneProcess = function (data, successCallback, errorCallback) {
            isRequesting = false;
            if (data) {
                if (data.targetUrl && data.targetUrl.url) {
                    Swal.close();
                    window.open(data.targetUrl.url, data.targetUrl.external ? '_blank' : '_self');
                    return;
                }
                countryApp.Message.Close();
                if (data.success && successCallback instanceof Function)
                    successCallback(data);
                else if (!data.success && errorCallback instanceof Function)
                    errorCallback(data);
            } else {
                countryApp.Message.Close();
            }
        };
        let failProcess = function (data, errorCallback) {
            countryApp.Message.Close();
            isRequesting = false;

            if (data && errorCallback instanceof Function) {
                errorCallback(data);
            }
        };

        this.Get = function (url, data, successCallback, errorCallback) {

            if (isRequesting) return;

            $.ajax({
                type: 'GET',
                url: url,
                beforeSend: function (request) {
                    isRequesting = true;
                    countryApp.Message.Loading();

                    if (token)
                        request.setRequestHeader("RequestVerificationToken", token.val());
                },
                data: data
            }).done(function (data) {
                doneProcess(data, successCallback, errorCallback);
            }).fail(function (data) {
                failProcess(data, errorCallback);
            });
        };

        this.Post = function (url, data, successCallback, errorCallback) {
            if (isRequesting) return;

            $.ajax({
                type: 'POST',
                url: url,
                beforeSend: function (request) {
                    countryApp.Message.Loading();
                    isRequesting = true;
                    if (token)
                        request.setRequestHeader("RequestVerificationToken", token.val());

                },
                data: data
            }).done(function (data) {
                doneProcess(data, successCallback, errorCallback);
            }).fail(function (data) {
                failProcess(data, errorCallback);
            });
        };

    },

    Message: new function () {
        let imageUrl = "/images/logos/logo-hexagon.png";
        let width = '40%';
        var calculateSwalWidth = function () {
            let windowWidth = $(window).width();
            if (windowWidth < 800 && windowWidth > 650) {
                width = '30%';
            }
            if (windowWidth < 650) {
                width = '40%';
            }
        };

        this.Loading = function (data) {
            calculateSwalWidth();
            var lang = document.cookie.indexOf("CurrentLanguage=ES") != -1;

            let modalTitle = data && data.title ? data.title : "";
            let modalMessage = data && data.message
                ? "<h4>" + data.message + "</h4>"
                : lang
                    ? "<h5>procesando solicitud...</h5>"
                    : "<h5>processing request...</h5>";

            let modalTimer = data && data.duration && data.duration > 0 ? data.duration : 0;

            Swal.fire({
                title: modalTitle,
                imageUrl: countryApp.Constants.BaseUrl + imageUrl,
                html: modalMessage,
                width: width,
                onOpen: () => {
                    Swal.showLoading();
                },
                allowEscapeKey: false,
                allowOutsideClick: false,
                timer: modalTimer
            });
        };

        this.Close = function () {
            Swal.close();
        };
    }
};

(function () {
    var showloader = function () {

        var loader = document.querySelector('.loader');
        var overlay = document.getElementById('overlayer');

        function fadeOut(el) {
            el.style.opacity = 1;
            (function fade() {
                if ((el.style.opacity -= .1) < 0) {
                    el.style.display = "none";
                } else {
                    requestAnimationFrame(fade);
                }
            })();
        };

        setTimeout(function () {
            fadeOut(loader);
            fadeOut(overlay);
        }, 200);
    };
    showloader();
})();

function createVueInstance(countryList)
{
    const app = Vue.createApp({
        data() {
            return {
                countries: JSON.parse(countryList),
                countriesPaginated: null,
                Region: null,
                SubRegion:null
            }
        },
        mounted: function () {
            
            //this.countries = null;
        },
        methods: {
            countryDetails(code) {
                //Filtrar desde countries
                //Validar que haya resultados
                //abrir modal con el detalle

                //cada vez que vaya a abrir un modal, cerrar el abierto en el momento
            },
            regionDetails(name) {
               
            },
            subregionDetails(name) {
                
            },
        },
    });

    app.mount("#app");
}

$(document).on('click', '.loading', function () {
    countryApp.Message.Loading();
});
