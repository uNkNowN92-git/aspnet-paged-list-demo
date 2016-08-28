select *
from authors a
left join books b
on a.AuthorId = b.AuthorId
where a.AuthorId > 9999