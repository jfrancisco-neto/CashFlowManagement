rootFolder=$(dirname $0)
cmfProject=$rootFolder/src/CashFlowManagement

start_compose()
{
    docker compose -f $rootFolder/deployment/docker/compose/compose.yaml up
}

create_account_migration()
{
    local projectPath=$cmfProject/Account
    echo "Creating migration for $projectPath"

    dotnet ef migrations add $1 --project $projectPath/Account.Persistence --startup-project $projectPath/Account.Api
}

remove_account_migration()
{
    local projectPath=$cmfProject/Account
    echo "Creating migration for $projectPath"

    dotnet ef migrations rm $1 --project $projectPath/Account.Persistence --startup-project $projectPath/Account.Api
}

$@
