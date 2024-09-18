# SandAndStonesEngine

Simple Engine to render 2D sprites in C# based on Veldrid library: https://github.com/veldrid/veldrid.

![Screen](https://github.com/user-attachments/assets/a93e05e7-6c5a-455f-b581-cfcdff8acd4e)

# Docker instructions

Run build, run tests for app in docker:

1. docker build -t sandengine .
2. docker run --rm -v ${pwd}:/src -w /src/SandAndStonesEngineTests mcr.microsoft.com/dotnet/sdk:8.0 dotnet test


## Author
- [Paweł Aksiutowicz](https://github.com/sandandstonesdev)
