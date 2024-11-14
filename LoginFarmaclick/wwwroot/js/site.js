// Incluir la función validarFormulario aquí
function validarFormulario() {
    var validEmail = /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/;
    var email = jQuery('#Email').val();

    if (!validEmail.test(email)) {
        alert('El email es inválido, no se enviará el formulario');
        return false;
    }

    const contraseña = document.getElementById("Contraseña").value;
    const regexContraseña = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$/;

    if (!regexContraseña.test(contraseña)) {
        alert("La contraseña debe tener al menos 8 caracteres, una letra mayúscula y un carácter especial.");
        return false;
    }

    alert('Email y contraseña son válidos, continúa con el envío del formulario');
    return true;
}

