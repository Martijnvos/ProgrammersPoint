var nieuwePosts = [];

$(window).load(function () {
    setInterval(CheckNieuwePosts, 10000);
});

function CheckNieuwePosts() {
    if (typeof (Storage) === "undefined") return;

    var mogelijk = CheckNotificatieMogelijkheid();
    
    if (mogelijk) {

        $.ajax({
            url: "/Home/HaalOngelezenPostsOp",
            success: function (result) {
                var alleNieuwePosts = JSON.parse(result);
                //Reset nieuwePosts zodat niet iedere keer dezelfde post terugkomt
                nieuwePosts = [];
                if (alleNieuwePosts != null) {
                    FilterNieuwePosts(alleNieuwePosts);
                    MaakEnStuurNotificatie();
                }
            },
            error: function (req, status, error) {
                // do something with error   
            }
        });
    }
};

function FilterNieuwePosts(alleNieuwePosts) {

    var gelezenPostsLocalStorage = JSON.parse(localStorage.getItem("gelezenPosts"));
    //Maak een nieuwe array aan als deze nog niet in localstorage te vinden is
    if (!Array.isArray(gelezenPostsLocalStorage)) {
        gelezenPostsLocalStorage = [];
    }
    //Check of het postId van de huidige nieuwe post zich al bevindt in de array.
    //Zo niet, voeg de complete nieuwe post dan toe aan de globale nieuwePosts array
    for (var i = 0; i < alleNieuwePosts.length; i++) {

        if (gelezenPostsLocalStorage.indexOf(alleNieuwePosts[i].PostId) < 0) {
            nieuwePosts.push(alleNieuwePosts[i]);
        }
    }
};

function CheckNotificatieMogelijkheid() {
    // Check of de browser notifications support
    if (!("Notification" in window)) return false;
       
    // Check of notificatie permissies al gegeven zijn
    else if (Notification.permission === "granted") {
        return true;
    }
    // Anders moeten we de gebruiker om permissie vragen
    else if (Notification.permission !== "denied") {
        Notification.requestPermission(function (permission) {
            if (permission === "granted") {
                return true;
            }
            return false;
        });
    }

    return false;
};

function MaakEnStuurNotificatie() {
    var postNotificatieTekst;
    if (nieuwePosts.length === 0) return;

    if (nieuwePosts.length === 1)
    {
        postNotificatieTekst = "Er is 1 nieuwe post beschikbaar: ";
    }
    else
    {
        postNotificatieTekst = "Er zijn " + nieuwePosts.length + " nieuwe posts beschikbaar: ";
    }

    for (var i = 0; i < nieuwePosts.length; i++)
    {
        //Laatste woord van de postslijst
        if (i === nieuwePosts.length - 1)
        {
            postNotificatieTekst += nieuwePosts[i].Naam + ".";
        }
        else
        {
            postNotificatieTekst += nieuwePosts[i].Naam + ", ";
        }
    }

    var notification = new Notification("Nieuwe post",
    {
        body: postNotificatieTekst,
        icon: "/images/Logo.png"
    });
    notification.onclick = function (event) {
        event.preventDefault();

        if (nieuwePosts.length === 1) {
            window.open("/Posts/Post/" + nieuwePosts[0].PostId, "_self");
        } else {
            window.open("/Posts/", "_self");
        }
        notification.close();
    }

    VoegGelezenPostsAanLocalStorageToe();
};

function VoegGelezenPostsAanLocalStorageToe() {
    var gelezenPostsLocalStorage = JSON.parse(localStorage.getItem("gelezenPosts"));
    if (!Array.isArray(gelezenPostsLocalStorage)) {
        gelezenPostsLocalStorage = [];
    }
    for (var i = 0; i < nieuwePosts.length; i++) {
        if (gelezenPostsLocalStorage.indexOf(nieuwePosts[i].PostId) < 0) {
            gelezenPostsLocalStorage.push(nieuwePosts[i].PostId);
            localStorage.setItem('gelezenPosts', JSON.stringify(gelezenPostsLocalStorage));
        }
    }
};