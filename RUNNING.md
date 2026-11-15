

<!-- local-development:start -->
# Desarrollo local

Puedes abrir este repositorio en VS Code, Antigravity o Sublime Text. El compilador y los servicios necesarios dependen del componente indicado abajo.

## Visual Studio 2022 / .NET Framework

Instala la carga de trabajo de escritorio .NET y el paquete de destino indicado en la tabla. Los proyectos ASP.NET también requieren la carga web e IIS Express. Abre la solución `.sln` correspondiente y selecciona el proyecto de aplicación para iniciar.

```powershell
# Ejecutar desde Developer PowerShell de Visual Studio 2022
$solutionDir = (Resolve-Path 'Proyecto para enviar/Proyecto para enviar/Proyecto/Juego').Path + '\'
MSBuild.exe 'Proyecto para enviar/Proyecto para enviar/Proyecto/Juego/Juego/Juego.csproj' /restore /p:RestorePackagesConfig=true /p:Configuration=Debug /p:DeployOnBuild=false "/p:SolutionDir=$solutionDir"
```

## Bases de datos

Revisa los scripts de esquema y crea una base de datos de prueba separada. SSMS 20 y pgAdmin 4 son clientes; SQL Server y PostgreSQL son motores distintos. Los scripts SQL Server no se ejecutan directamente en PostgreSQL. Las restauraciones deben hacerse sobre una base de prueba nueva. PostgreSQL 17 y 18 requieren comprobarse por separado.

## Componentes y validación

Inventario y compilaciones locales del 2026-11-15. Una compilación correcta no acredita todavía interacción, compatibilidad de base de datos, instalador ni despliegue.

| Componente | Tipo / destino | Validación |
| --- | --- | --- |
| [Proyecto para enviar/Proyecto para enviar/Proyecto/Juego/Juego.sln](<Proyecto para enviar/Proyecto para enviar/Proyecto/Juego/Juego.sln>) | solution | Sin ejecutar |
| [Proyecto para enviar/Proyecto para enviar/Proyecto/Juego/Juego/Juego.csproj](<Proyecto para enviar/Proyecto para enviar/Proyecto/Juego/Juego/Juego.csproj>) | dotnet / v4.8.1 | Compila; comprobado el 2026-11-15; ver alcance actualizado abajo |
| [Proyecto-Sistemas-Operativos-master/Juego/Juego.sln](<Proyecto-Sistemas-Operativos-master/Juego/Juego.sln>) | solution | Sin ejecutar |
| [Proyecto-Sistemas-Operativos-master/Juego/Juego/Juego.csproj](<Proyecto-Sistemas-Operativos-master/Juego/Juego/Juego.csproj>) | dotnet / v4.8.1 | Compila; comprobado el 2026-11-15; ver alcance actualizado abajo |

## Archivos ignorados

`.gitignore` excluye credenciales locales, estado del editor y salidas de las herramientas detectadas. No deja de versionar archivos que ya estaban en Git. Los manifiestos, archivos de bloqueo, recursos fuente y scripts de esquema forman parte del código.
<!-- local-development:end -->

<!-- source-findings:start -->
## Hallazgos en los archivos fuente

Hallazgos de inspección estática; deben revisarse aunque otro componente compile.

| Hallazgo | Archivo |
| --- | --- |
| Fuente compuesta solo de bytes cero | `Proyecto-Sistemas-Operativos-master/Juego/Juego/frmNuevo_Jugador.cs` |
<!-- source-findings:end -->

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
