//
// XMLHttpRequest Observation
//
// http://www.w3.org/TR/XMLHttpRequest/
// http://www.w3.org/TR/XMLHttpRequest2/
//
(function () {
    // Original XMLHttpRequest
    cefGlue.XMLHttpRequest = cefGlue.XMLHttpRequest || window.XMLHttpRequest;

    cefGlue._activeRequests = 0;

    if (cefGlue["activeRequests"] === undefined) {
        Object.defineProperty(cefGlue, "activeRequests", {
            get: function() { return this._activeRequests; },
            set: function(value) {
                this._activeRequests = value;
                console.debug("cefGlue.activeRequests = " + value);
                if (value==0) cefGlue._browser.notifyActiveRequests(value);
            }
        });
    }

    var log = true;

    var XMLHttpRequest = function XMLHttpRequest() {
        if (log) console.debug("XMLHttpRequest()");
        this._xhr = new cefGlue.XMLHttpRequest();

        var self = this;
        var dispatch = function(event) { self.dispatchEvent(event); };

        this._xhr.onabort = dispatch;
        this._xhr.onerror = dispatch;
        this._xhr.onload = dispatch;
        this._xhr.onloadstart = dispatch;
        this._xhr.onprogress = dispatch;
        this._xhr.onreadystatechange = function(event) {
            // TODO: execute this after onload...
            if (self.readyState === XMLHttpRequest.DONE) { cefGlue.activeRequests--; }
            self.dispatchEvent(event);
        };

        this._listeners = [];
        this._events = {};
    };

    XMLHttpRequest.DONE = 4;
    XMLHttpRequest.HEADERS_RECEIVED = 2;
    XMLHttpRequest.LOADING = 3;
    XMLHttpRequest.OPENED = 1;
    XMLHttpRequest.UNSENT = 0;

    XMLHttpRequest.prototype = {
        DONE: XMLHttpRequest.DONE,
        HEADERS_RECEIVED: XMLHttpRequest.HEADERS_RECEIVED,
        LOADING: XMLHttpRequest.LOADING,
        OPENED: XMLHttpRequest.OPENED,
        UNSENT: XMLHttpRequest.UNSENT,

        get onabort() {
            return this._getEvent("abort");
        },
        set onabort(value) {
            this._setEvent("abort", value);
        },

        get onerror() {
            return this._getEvent("error");
        },
        set onerror(value) {
            this._setEvent("error", value);
        },
        
        get onload() {
            return this._getEvent("load");
        },
        set onload(value) {
            this._setEvent("load", value);
        },

        get onloadstart() {
            return this._getEvent("loadstart");
        },
        set onloadstart(value) {
            this._setEvent("loadstart", value);
        },
        
        get onprogress() {
            return this._getEvent("progress");
        },
        set onprogress(value) {
            this._setEvent("progress", value);
        },

        get onreadystatechange() {
            return this._getEvent("readystatechange");
        },
        set onreadystatechange(value) {
            this._setEvent("readystatechange", value);
        },

        get readyState() { return this._xhr.readyState; },
        get response() { return this._xhr.response; },
        get responseText() { return this._xhr.responseText; },
        
        get responseType() { return this._xhr.responseType; },
        set responseType(value) { this._xhr.responseType = value; },

        get responseXML() { return this._xhr.responseXML; },
        get status() { return this._xhr.status; },
        get statusText() { return this._xhr.statusText; },
        
        get withCredentials() { return this._xhr.withCredentials; },
        set withCredentials(value) { this._xhr.withCredentials = value; },

        // TODO: ...
        // timeout

        abort: function () {
            if (log) console.debug("abort()");
            this._xhr.abort();
        },

        getAllResponseHeaders: function () {
            if (log) console.debug("getAllResponseHeaders()");
            return this._xhr.getAllResponseHeaders();
        },
        
        getResponseHeader: function (header) {
            if (log) console.debug("getResponseHeader()");
            return this._xhr.getResponseHeader(header);
        },

        open: function (method, url, async, user, password) {
            if (log) console.debug("open(): " + method +" " + url + " " + async + " " + user + " " + password);
            var ex;
            try {
                this._xhr.open.apply(this._xhr, arguments);
            } catch (e) {
                ex = e;
            }
            if (!ex) cefGlue.activeRequests++;
            else throw ex;
        },

        overrideMimeType: function (mime) {
            if (log) console.debug("overrideMimeType()");
            this._xhr.overrideMimeType(mime);
        },

        send: function (data) {
            if (log) console.debug("send(): " + data);
            this._xhr.send.apply(this._xhr, arguments);
        },

        setRequestHeader: function (header, value) {
            if (log) console.debug("setRequestHeader(): " + header + " = " + value);
            this._xhr.setRequestHeader(header, value);
        },

        // TODO: upload: XMLHttpRequestUpload;


        // EventTarget

        addEventListener: function (type, listener, useCapture) {
            if (log) console.debug("addEventListener(): " + type);

            if (this._listeners.some(
                    function(e){ return e.type === type && e.listener === listener; }
                    )
                ) {
                return;
            }

            this._listeners.push( { type: type, listener: listener } );
        },

        removeEventListener: function (type, listener, useCapture) {
            if (log) console.debug("removeEventListener(): " + type);

            for (var i = 0; i < this._listeners.length; i++) {
                var el = this._listeners[i];
                if (el.type === type && el.listener === listener) {
                    this._listeners.splice(i, 1);
                    return;
                }
            }
        },

        dispatchEvent: function (event) {
            if (log) console.debug("dispatchEvent(): " + event.type);

            var evt = null;

            for (var i = 0; i < this._listeners.length; i++) {
                var el = this._listeners[i];
                if (el.type === event.type) {
                    if (evt == null) {
                        evt = Object.create(event, { target: { value: this } });
                    }

                    setTimeout(
                        function(listener, evt){
                            return function(){ listener.call(evt.target, evt); }
                        }(el.listener, evt),
                        0);

                    // el.listener.call(evt.target, evt); // TODO: handle exceptions
                }
            }
        },

        _getEvent: function(type) {
            if (log) console.debug("_getEvent: " + type);
            return this._events[type];
        },

        _setEvent: function(type, listener) {
            if (log) console.debug("_setEvent: " + type);
            if (this._events[type]) {
                this.removeEventListener(type, this._events[type]);
                this._events[type] = null;
            }
            this._events[type] = listener;
            this.addEventListener(type, listener);
        },
    };

    window.XMLHttpRequest = XMLHttpRequest;
} ());
//@ sourceURL=CefGlue/XMLHttpRequest.js
