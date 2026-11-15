# Juego-Mata-al-Hongo-de-Mario-Bros--sin-sonido----Minor-Rojas-J.---U.Cat---C-

<!-- local-development:start -->
## Desarrollo local

Consulta [RUNNING.md](RUNNING.md) para ubicar cada componente, abrirlo en su IDE, compilarlo y conocer las comprobaciones realizadas y los requisitos pendientes.
<!-- local-development:end -->

<!-- build-repairs-20261115:start -->
## Reparación y comprobación — 2026-11-15

- Se reconstruyó el código del formulario que contenía solo bytes NUL, con validación de entradas y devolución del nombre del jugador.
- Se completó el cálculo de memoria física y el evento Load; el temporizador queda asociado al contenedor de componentes para liberarlo al cerrar.

Comprobado:

- Compilan las dos variantes .NET.
- Se construyeron los cuatro formularios de la variante reparada y se comprobaron sus recursos.

Comandos:

```powershell
msbuild 'Proyecto-Sistemas-Operativos-master/Juego/Juego.sln' /t:Build
```

Alcance pendiente:

- El registro de jugadores de esta variante continúa siendo un prototipo sin servicio de cuentas ni persistencia de contraseñas. La otra variante conserva sus requisitos de base de datos.

<!-- build-repairs-20261115:end -->
