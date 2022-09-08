# Build

Simple build (`ci` build done on CI Azure DevOps servers):

```
dotnet cake -t=ci
```

Clean `ci` build followed by nuget API diff and then several utility tqrgets:

On MacOSX (and Linux):

```bash
dotnet cake -t=clean && dotnet cake -t=ci && dotnet cake nuget-diff.cake && dotnet cake utilities.cake
```

## Build (`build.cake`)

*   `javadocs`

    Prepares javadocs for parameter names.

*   `tools-update`

    Updates some of the tools.

*   `binderate`

    Runs `binderator` on `config.json` data.

*   `binderate-prepare-dependencies-samples-packagereferences`

*   `binderate-prepare-dependencies-samples-packages-config`

*   `binderate-config-verify`

    Verifies versions in `config.json`

*   `binderate-diff`

    Runs `diff` to see details of the update.

*   `binderate-fix`

*   `mergetargets`

*   `libs-native`

    Builds native maven projects with gradle.

*   `libs`

    Builds projects (assemblies - libraries - libs)

*   `samples-directory-build-targets`

    Generates list of locally built packages which are tested during samples builds.

*   `samples`

    Builds samples

*   `allbindingprojectrefs`

*   `nuget`

    Generates (packaging) NuGet packages for projects.

*   `merge`

    Merges all assemblies/projects into single assembly for NuGet API diff.

*   `ci-setup`

    Sets up CI environment.

*   `nuget-dependecies`

*   `genapi`

*   `docs-api-diff`

    Generates MarkDown docs from API diff XML files.

*   `clean`

    Cleans folders and files.

*   `ci`

    Builds projects on CI (`libs`, `nuget`, `samples`).

## Nuget API diff (`nuget-diff.cake`)

Generates API diff (XML and MarkDown) files for differences between local packages and latest packages from `nuget.org`.

## Utilities (`utilities.cake`)

*   `generate-component-governance`

    Generates Components Governance `cgmanifest.json`

*   `mappings-artifact-nuget`

    Generates mapping maven artifacts to nuget packages.

*   `list-artifacts`

    Generate list of the maven artifacts and its nuget package.

*   `spell-check`

    Spell checks namespaces and nuget packages.

*   `namespace-check`

    Verifies namespaces (names and casing)

*   `binderate-diff`

    Runs `diff` to see details of the update.

*   `target-sdk-version-check`

    Verification of TFMs.

*   `api-diff-markdown-info-pr`

    Generates Markdown about update info for PRs (weekly stable updates)

*   `api-diff-analysis`

    Generates Markdown about update info for PRs (weekly stable updates)

*   `nuget-structure-analysis`

    Unpacks nuget packages, so the structure can be verified.

*   `read-analysis-files`

    Opens analysis files (API diff, spell checking, etc) VS Code required.

*   `dependencies`

    WIP: dependency trees generation (Maven and Nuget)

*   `generate-markdown-publish-log`

    Generates MarkDown based on CI NuGet publish log.

*   `Default`

    Default target - `ci`
    