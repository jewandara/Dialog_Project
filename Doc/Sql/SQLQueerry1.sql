
WITH _tempTable AS
(
    SELECT *,ROW_NUMBER() OVER (ORDER BY LoginID) AS _rowNum
    FROM dialog_Login
)
SELECT *
FROM _tempTable
WHERE _rowNum BETWEEN 2 AND 3
