To use the GameFramework you need to do the following setup:

1) Create a new class that inherits from GameBase
2) Create a prefab with this newly created class as it's (only) component
3) Assign this prefab in the GameFrameworkSettings scriptable object

Do NOT instantiate or drag the prefab into any scene, this will be done automatically before scene load (before Awake)