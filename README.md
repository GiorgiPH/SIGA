# SIGA - Sistema Integral de Gestión Administrativa

Aplicación de escritorio (ERP) desarrollada en C# sobre .NET Framework, orientada a la gestión integral de procesos **financieros, administrativos, contables y operativos** (condominios, inventarios, compras, cobranza, presupuestos, activos fijos, entre otros).

## Tabla de contenido

- [Arquitectura](#arquitectura)
- [Estructura del repositorio](#estructura-del-repositorio)
- [Tecnologías](#tecnologías)
- [Requisitos previos](#requisitos-previos)
- [Instalación y puesta en marcha](#instalación-y-puesta-en-marcha)
- [Configuración de la base de datos](#configuración-de-la-base-de-datos)
- [Compilación del instalador](#compilación-del-instalador)

## Arquitectura

El proyecto sigue una arquitectura de **dos capas**:

1. **Presentación + Lógica de negocio**: formularios Windows Forms (`.cs` / `.Designer.cs` / `.resx`) junto con las clases de negocio ubicadas en [PV/Clases/](PV/Clases/), organizadas por módulo (Condominios, Inventario, Personal, Documentos, etc.).
2. **Acceso a datos (DAO)**: clases encargadas de la conexión y operaciones contra la base de datos mediante **ADO.NET**.

Los reportes se generan con **Microsoft Reporting Services / RDLC** (`.rdlc`), embebidos en los formularios de reporte.

## Estructura del repositorio

```
SIGA/
├── PV/                 # Proyecto principal (WinForms) - presentación + lógica de negocio + DAO
│   ├── Clases/          # Clases de negocio y acceso a datos por módulo
│   ├── Resources/        # Recursos e imágenes
│   └── *.rdlc            # Plantillas de reportes
├── SIGA/                # Proyecto de instalador (Setup) - versión completa
├── SIGA Lite/           # Proyecto de instalador (Setup) - versión reducida
├── Iconos/               # Iconos utilizados en la aplicación
├── packages/             # Paquetes NuGet restaurados
└── Sistema Integral de Gestion Administrativa.sln   # Solución de Visual Studio
```

## Tecnologías

- C# / .NET Framework 4.7.1
- Windows Forms
- ADO.NET (acceso a datos)
- Microsoft Reporting Services (RDLC)
- NuGet (gestión de dependencias)
- Visual Studio Installer Projects (generación de ejecutables/instaladores)

## Requisitos previos

- **Visual Studio 2019/2022** con la carga de trabajo de desarrollo de escritorio con .NET.
- **.NET Framework 4.7.1 Developer Pack**.
- **SQL Server** (o el motor de base de datos configurado en el proyecto) accesible desde el equipo de desarrollo.
- Extensiones de Visual Studio (ver siguiente sección).

## Instalación y puesta en marcha

Sigue estos pasos en orden después de clonar el repositorio:

### 1. Clonar el repositorio

```bash
git clone <url-del-repositorio>
```

### 2. Instalar extensiones de Visual Studio

Antes de abrir la solución, instala las siguientes extensiones desde **Extensiones > Administrar extensiones** en Visual Studio:

- **Microsoft Reporting Services Projects 2022** (o la versión correspondiente al proyecto) — necesaria para trabajar con los reportes `.rdlc`.
- **Microsoft Visual Studio Installer Projects** — necesaria para compilar los proyectos `SIGA` y `SIGA Lite` (generación de ejecutables/instaladores `.msi`).

> Una vez instaladas ambas extensiones, **cierra Visual Studio por completo** y espera a que finalice el proceso de instalación antes de volver a abrirlo.

### 3. Abrir la solución

Vuelve a abrir Visual Studio y carga el archivo de solución:

```
Sistema Integral de Gestion Administrativa.sln
```

### 4. Restaurar paquetes NuGet

En el **Explorador de soluciones**, haz clic derecho sobre la solución (nodo raíz) y selecciona:

```
Restaurar paquetes NuGet
```

### 5. Configurar la cadena de conexión

Revisa y ajusta la cadena de conexión a la base de datos en el archivo `App.config` del proyecto `PV` según tu entorno local (ver [Configuración de la base de datos](#configuración-de-la-base-de-datos)).

### 6. Ejecutar la aplicación

Selecciona el proyecto `PV` (Sistema Integral de Gestion Administrativa) como **proyecto de inicio** y ejecuta con `F5` o el botón de Iniciar.

## Configuración de la base de datos

El acceso a datos se realiza mediante clases DAO con ADO.NET. Verifica en `PV/App.config` los parámetros de conexión (servidor, base de datos, credenciales) antes de ejecutar el proyecto, y asegúrate de que la base de datos requerida esté disponible y con el esquema correspondiente.

## Compilación del instalador

Los proyectos `SIGA` y `SIGA Lite` son proyectos de tipo **Setup (Installer Project)** y requieren la extensión *Visual Studio Installer Projects* instalada (ver paso 2). Para generar el ejecutable de instalación:

1. Haz clic derecho sobre el proyecto `SIGA` o `SIGA Lite` en el Explorador de soluciones.
2. Selecciona **Compilar** (Build).
3. El instalador `.msi` se generará en la carpeta `Debug` o `Release` del proyecto correspondiente.
