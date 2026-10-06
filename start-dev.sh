#!/bin/bash

echo "🚀 Starting IPFlow Development Environment..."

cd "$(dirname "$0")"

echo "📦 Starting Backend..."
cd Backend/IPFlowAPI
dotnet run &
BACKEND_PID=$!

sleep 3

echo "🎨 Starting Frontend..."
cd ../../Frontend/ipflow-app
npm start &
FRONTEND_PID=$!

echo ""
echo "✅ IPFlow is starting..."
echo "📍 Backend: http://localhost:5006"
echo "📍 Frontend: http://localhost:4200"
echo "📍 Swagger: http://localhost:5006/swagger"
echo ""
echo "Press Ctrl+C to stop both servers"

wait
