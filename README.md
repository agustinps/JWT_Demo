# JWT_Demo

## Demostración de uso de JSon Web Token para autenticación y autorización

JWT nos permite de forma segura trasmitir datos entre dos partes en forma de un objeto JSON. Es un estandar abierto donde la información está cifrada, verificada y se confía en ella 
y es un mecanisto muy estendido para la autenticación web. un JWT tiene tres partes :

	1 Cabecera, que está cifrada en base 64 e incluye el algoritmo de cifrado y el tipo de token usado.
	
	2 Payload (o carga útil), también codificado en base 64 y contiene atributos sobre el usuario. Nunca debemos incluir información confidencial porque no está encriptado.
	
	3 Firma, se utiliza para verificar si el token es válido, se genera combinando la cabecera y el payload y se basa en una clave secreta que sólo el servidor debe conocer.
	
En primer lugar creamos el método de extensión AddJWTValidation dentro de la clase ServiceExtensions para tener un código más legible en nuestra clase program, donde posteriormente añadimos este método.

Creamos una clase AuthenticationService donde generamos el token en el supuesto de que las credenciales del usuario sean correctas, en nuestro caso y puesto que es una simulación, siempre lo serán.

Finalmente creamos dos controladores, uno para recibir la petición de login "LoginController" y que llamrá a nuestra clase anterior y otro, "TestAuthorizationController" que utilizaremos para hacer pruebas :

	1 AuthenticationFree, no requiere estar autenticado, es decir, acceso anonimo, que podría estar identificado con el atributo [AllowAnonymous].
	
	2 AuthenticationRequired, necesita estar autenticado.
	
	3 AuthenticationRequiredV2, necesita estar autenticado y muestra como manejar la información del usuario almacenada en sus claims.
	
	4 AuthorizationRoleAdminRequired, necesita estar autenticado y tener autorización mediante el role "administrator", como es nuestro usuario de ejemplo.
	
	5 AuthorizationRoleUserRequired, necesita estar autenticado y tener autorización mediante role "user", en nuestro caso no tendremos acceso a pesar de estar autenticado con usuario y contraseña.


Según la funcionalidad deseada, si el token caduca, podemos enviar al cliente a la pagina de logín, o podemos generar un nuevo token (RefreshToken) en cada petición, para ampliar la caducidad y enviarlo en al respuesta.