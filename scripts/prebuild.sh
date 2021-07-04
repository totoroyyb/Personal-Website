#!/bin/sh -l

wget https://dotnet.microsoft.com/download/dotnet/scripts/v1/dotnet-install.sh .
./install-dotnet.sh --version 3.1.410
./install-dotnet.sh --version 5.0.301