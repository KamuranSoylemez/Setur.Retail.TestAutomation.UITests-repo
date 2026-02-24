#!/bin/bash

# Fast test runner with real-time progress display
# Usage: ./run-tests-fast.sh [TestClassName]

TEST_FILTER="${1:-}"
PROJECT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)/RetailTRUI.Tests"

cd "$PROJECT_DIR" || exit 1

echo "🚀 Starting tests with real-time progress..."
echo "================================================"
echo ""

if [ -z "$TEST_FILTER" ]; then
    echo "Running ALL tests..."
    dotnet test --logger "console;verbosity=normal" 2>&1 | \
        grep --line-buffered -E "Passed|Failed|Starting|Finished|Total tests:|Passed:|Failed:" | \
        while IFS= read -r line; do
            if [[ $line == *"Passed"* ]]; then
                echo "✅ $line"
            elif [[ $line == *"Failed"* ]]; then
                echo "❌ $line"
            else
                echo "$line"
            fi
        done
else
    echo "Running tests: $TEST_FILTER..."
    dotnet test --filter "FullyQualifiedName~$TEST_FILTER" --logger "console;verbosity=normal" 2>&1 | \
        grep --line-buffered -E "Passed|Failed|Starting|Finished|Total tests:|Passed:|Failed:" | \
        while IFS= read -r line; do
            if [[ $line == *"Passed"* ]]; then
                echo "✅ $line"
            elif [[ $line == *"Failed"* ]]; then
                echo "❌ $line"  
            else
                echo "$line"
            fi
        done
fi

echo ""
echo "================================================"
echo "✨ Test run complete!"




