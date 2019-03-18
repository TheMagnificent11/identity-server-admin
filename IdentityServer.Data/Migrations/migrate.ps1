Param(
    [Parameter(ValueFromPipelineByPropertyName = $true, Position = 0, Mandatory = $true)]
    [string]$ConnectionString,

    [Parameter(ValueFromPipelineByPropertyName = $true, Position = 1, Mandatory = $true)]
    [string]$InputFile
)

Invoke-Sqlcmd -ConnectionString $ConnectionString -InputFile $InputFile -OutputSqlErrors $true