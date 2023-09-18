function showSignInForm() {
    document.getElementById("loginForm").style.display = "block";
    document.getElementById("registerForm").style.display = "none";
    document.getElementById("recoverForm").style.display = "none";
    document.getElementById("changeForm").style.display = "none";
    document.getElementById("returnLink").style.display = "none";
    document.getElementById("recoverLink").style.display = "block";
}

function showRegisterForm() {
    document.getElementById("loginForm").style.display = "none";
    document.getElementById("registerForm").style.display = "block";
    document.getElementById("recoverForm").style.display = "none";
    document.getElementById("changeForm").style.display = "none";
    document.getElementById("returnLink").style.display = "none";
    document.getElementById("recoverLink").style.display = "block";
}

function showRecoverForm() {
    document.getElementById("loginForm").style.display = "none";
    document.getElementById("registerForm").style.display = "none";
    document.getElementById("recoverForm").style.display = "block";
    document.getElementById("changeForm").style.display = "none";
    document.getElementById("returnLink").style.display = "block";
    document.getElementById("recoverLink").style.display = "none";
}