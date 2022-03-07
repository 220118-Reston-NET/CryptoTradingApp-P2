function submitRegistration() {
    const name = document.getElementById("fullname");
    const username = document.getElementById("username");
    const age = document.getElementById("age");
    const password = document.getElementById("password");
    const repeat = document.getElementById("repassword");

    if (name.value.length < 2) {
        window.alert("Name length should be more than 2.");
        name.focus();
        return false;
    }
    if (isNumber(name.value)) {
        window.alert("Numerical values in a name is not allowed.");
        name.focus();
        return false;
    }
    if (username.value.length < 4) {
        window.alert("Username length should be more than 4.");
        username.focus();
        return false;
    }
    if (age.value < 18 || age.value > 120) {
        window.alert("You must be at-least 18 years old.");
        age.focus();
        return false;
    }
    if (!password.value.match(/^.{5,15}$/)) {
        alert("Password length must be between 5-15 characters!");
        password.focus();
        return false;
    }
    if (repeat.value != password.value) {
        alert("Passwords do not match!");
        repeat.focus();
        return false;
      }
}

function submitLogin() {
    const username = document.getElementById("username");
    const password = document.getElementById("password");
}

function isNumber(n) {
    return !isNaN(parseFloat(n)) && !isNaN(n - 0);
}