#!/bin/bash

cd /src/XSquareCalculationsApi
dotnet ef database update
cd /app/publish
dotnet XSquareCalculationsApi.dll