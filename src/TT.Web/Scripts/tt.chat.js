﻿var ChatModule = (function () {
    var unreadCount = 0;
    var cooldownActive = false;
    var connected = false;
    var roomName = '';
    var reconnectTimer;
    var connectAttempts = 0;
    var maxConnectAttempts = 20;
    
    var pub = {};

    var audioModes = {
        all: 'all',
        some: 'some',
        none: 'none'
    };

    var oldAudioModes = {
        0: 'none',
        1: 'some',
        2: 'all'
    };

    var connectionStatus = {
        connectionSlow: "We are currently experiencing difficulties with your connection, chat messages may be delayed",
        reconnecting: "Chat has been disconnected, trying to reconnect...",
        disconnected: "Chat has been disconnected and failed to reconnect, please re-load the page"
    };

    pub.chat = $.connection.chatHub;

    /* Private methods */

    function updateTitle(title) {
        $(document).attr('title', title);
    }

    function doConfig() {
        if (ConfigModule.chat.roomConfig[roomName] === undefined) {
            var audioMode = audioModes.none;

            if (localStorage['chat_sound_' + roomName] !== undefined) {
                audioMode = audioModes[oldAudioModes[localStorage['chat_sound_' + roomName]]];
                localStorage.removeItem(['chat_sound_' + roomName]);
            }

            ConfigModule.chat.roomConfig[roomName] = {
                audioMode: audioMode
            }

            ConfigModule.save();
        }

        if (ConfigModule.chat.autoScrollEnabled)
            $('#autoScrollToggle').text('Autoscroll ON');
        else
            $('#autoScrollToggle').text('Autoscroll OFF');
    }

    function playAudio(isAlert) {

        var audioMode = ConfigModule.chat.roomConfig[roomName].audioMode;

        if (audioMode === audioModes.none)
            return;

        if (audioMode === audioModes.some && isAlert === false)
            return;

        var popAudio;

        if (isAlert === true)
            popAudio = new Audio('../../Sounds/pop2.ogg');
        else
            popAudio = new Audio('../../Sounds/pop.ogg');

        popAudio.play();
    }

    function showConnectionStatus(status) {
        $('#connectionStatus').text(status).show();
    }

    function hideConnectionStatus() {
        $('#connectionStatus').hide();
    }

    function connect() {
        $.connection.hub.start().done(onChatHubStarted);
        $.connection.hub.connectionSlow(function () { showConnectionStatus(connectionStatus.connectionSlow); });
        $.connection.hub.reconnecting(onReconnecting);
        $.connection.hub.disconnected(onChatDisconnected);
        $.connection.hub.reconnected(hideConnectionStatus);
    }
    
    /* Event handlers */

    function onGainFocus() {
        if (connected)
            updateTitle(roomName);

        unreadCount = 0;
    }

    function onSendMessage() {
        if (cooldownActive === false) {
            pub.chat.server.send($('#message').val());
            $('#message').val('').focus();
        }

        cooldownActive = true;

        window.setInterval(function() { cooldownActive = false; }, 3000);
    }

    function onChatHubStarted() {
        pub.chat.server.joinRoom(roomName);
        pub.chat.state.toRoom = roomName;

        $(window).focus(onGainFocus);
        $('#sendmessage').click(onSendMessage);

        $(document).keypress(function(e) { 
            if (e.which === 13) {
                onSendMessage();
            }
        });

        connectAttempts = 0;
        hideConnectionStatus();
        updateTitle(roomName);
        window.clearInterval(reconnectTimer);
        connected = true;
    }

    function onReconnecting() {
        showConnectionStatus(connectionStatus.reconnecting);
        updateTitle('(Reconnecting) ' + roomName);
    }

    function onChatDisconnected() {
        if (connected === false)
            return;

        connected = false;

        reconnectTimer = setInterval(function () {
            if (connected === false && connectAttempts < maxConnectAttempts) {
                connectAttempts++;
                showConnectionStatus(connectionStatus.reconnecting + ' (attempt ' + connectAttempts + '/' + maxConnectAttempts + ')');
                updateTitle('(Reconnecting) ' + roomName);

                connect();
            } else {
                showConnectionStatus(connectionStatus.disconnected);
                updateTitle('(Disconnected) ' + roomName);
                window.clearInterval(reconnectTimer);
            }
        }, 30000);
    }

    function onNewMessage(model) {
        var output = ChatMessageModule.formatMessage(model);
        $('#discussion').append($(output));

        if (ConfigModule.chat.autoScrollEnabled)
            $('#discussion ').animate({ scrollTop: $('#discussion').prop("scrollHeight") }, 500);

        playAudio(model.Message.indexOf(pub.currentPlayer) > 0);

        if (!document.hasFocus()) {
            unreadCount++;
            updateTitle('(' + unreadCount + ') ' + roomName);
        }
    }

    function onNewIgnoreAdded(newIgnore) {
        ConfigModule.chat.ignoreList.push(newIgnore);
        ConfigModule.save();

        $("#ignore").val("");
        alert("Now ignoring:  " + newIgnore);
    }

    function onIgnoreReset() {
        ConfigModule.chat.ignoreList = [];
        ConfigModule.save();

        alert("No longer ignoring anyone.");
    }

    function onIgnoreViewed() {
        $('#discussion ').append('<li><b>You will not see any chat messages that contain the following substrings:</b></li>');
        if (ConfigModule.chat.ignoreList.length > 0) {
            for (var x = 0; x < ConfigModule.chat.ignoreList.length; x++) {
                $('#discussion ').append('<li>' + ConfigModule.chat.ignoreList[x] + ' </li>');
            }
        } else {
            $('#discussion ').append('<li><i>You do not have anyone or anything on your ignore list.</i></li>');
        }
    }

    function onNameChanged(newName) {
        pub.currentPlayer = newName;
    }

    /* Public methods */

    pub.initialize = function (options) {
        roomName = options.roomName;
        pub.currentPlayer = options.currentPlayer;
        pub.currentPlayerChatColor = options.currentPlayerChatColor;

        doConfig();

        connect();

        pub.chat.client.addNewMessageToPage = onNewMessage;
        pub.chat.client.nameChanged = onNameChanged;
    }

    pub.onUserDoubleTapped = function (name, e) {
        $('#message').val($('#message').val() + "@" + name + ' ');
        $('#message').focus();

        e.preventDefault();
    }

    /* Events */

    $("#autoScrollToggle").click(function () {
        if (ConfigModule.chat.autoScrollEnabled === false) {
            ConfigModule.chat.autoScrollEnabled = true;
            $(this).text("Autoscroll ON");
        } else {
            ConfigModule.chat.autoScrollEnabled = false;
            $(this).text("Autoscroll OFF");
        }

        ConfigModule.save();
    });

    $("#toggleImages").click(function () {
        if (ConfigModule.chat.imagesEnabled === false) {
            alert('Images enabled');
            ConfigModule.chat.imagesEnabled = true;
        } else {
            alert('Images disabled');
            ConfigModule.chat.imagesEnabled = false;
        }

        ConfigModule.save();
    });

    $('li.audioConfig').click(function() {
        var newMode = $(this).attr('data-audio-mode');

        ConfigModule.chat.roomConfig[roomName].audioMode = newMode;
        ConfigModule.save();

        var alertText = {
            'all': 'All sounds enabled for this room.',
            'some': 'Some sounds enabled for this room.  You will only hear a sound if somone addresses you in particular.',
            'none': 'All sounds disabled for this room'
        };

        alert(alertText[newMode]);
    });

    $("#showIgnore").click(function () {
        if ($("#ignoreDiv").is(":visible") === false) {
            $("#ignoreDiv").show();
        } else {
            $("#ignoreDiv").hide();
        }
    });

    $("#ignoreAdd").click(function () {
        onNewIgnoreAdded($("#ignore").val());
    });

    $('#ignoreReset').click(function() {
        onIgnoreReset();
    });

    $("#ignoreView").click(function () {
        onIgnoreViewed();
    });

    return pub;
})();