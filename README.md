**Simple code listing generator using WinUI 3 framework and .NET 10.**  
You just need to specify the project folder, and the program will recursively traverse all subfolders, read all files with selected extensions, and copy their contents into a `README.md` file in a convenient Markdown format with a table of contents and links.

**Supported file formats:**
- `.py`
- `.xaml`
- `.cs`
- `.csproj`
- `.json`
- `.manifest`
- `.kt`
- `.gradle`
- `.kts`
- `.xml`
- `.properties`
- `.java`
- `.jar`
- `.arr`
- `.toml`

**Default blacklisted folders** (mostly library and cache folders):

- **Python**
  - `__pycache__`
  - `.venv`
  - `venv`
  - `env`
  - `virtualenv`
  - `.pytest_cache`
  - `.mypy_cache`
  - `.tox`
  - `site-packages`
  - `dist-packages`

- **.NET / C#**
  - `bin`
  - `obj`
  - `.vs`
  - `packages`
  - `.nuget`
  - `AppPackages`
  - `BundleArtifacts`

- **Java / Kotlin / Gradle**
  - `.gradle`
  - `build`
  - `target`
  - `.idea`
  - `gradle`
  - `out`
  - `libs`
  - `classes`

- **Node.js / JavaScript / TypeScript**
  - `node_modules`
  - `.npm`
  - `.yarn`
  - `dist`
  - `build-dist`
  - `.cache`
  - `.parcel-cache`
  - `.next`
  - `.nuxt`
  - `.output`

- **Git / Version Control**
  - `.git`
  - `.svn`
  - `.hg`
  - `.github`
  - `.gitlab`
  - `.vscode`

- **IDE folders**
  - `.vscode`
  - `.idea`
  - `.vs`
  - `.settings`
  - `Debug`
  - `Release`
  - `x64`
  - `x86`
  - `AnyCPU`

- **General cache and temp folders**
  - `.cache`
  - `cache`
  - `temp`
  - `tmp`
  - `.temp`
  - `.tmp`
  - `logs`
  - `.logs`

- **Mobile development**
  - `build`
  - `libs`
  - `generated`
  - `flutter`
  - `.pub-cache`

- **Documentation output**
  - `docs`
  - `Documentation`
  - `doc`
  - `apidoc`

- **Test related**
  - `coverage`
  - `.coverage`
  - `TestResults`
  - `__test__`
  - `tests-output`

- **Package managers**
  - `packages`
  - `.packages`
  - `Library`
  - `Frameworks`

---

Currently, the main development is happening in the `unpacked` branch. More updates may come in the future. Also, in the near future, I will either update or remove the `master` branch with the package build.