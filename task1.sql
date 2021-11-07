create table task1
(
	id int auto_increment,
	my_date DATE not null,
	string_en varchar(10) not null,
	string_ru varchar(10) not null,
	my_int int not null,
	my_double double not null,
	constraint task1_pk
		primary key (id)
);



create procedure SumInt ()
begin
select Sum(my_int) from task1;
end;

create procedure MedianDouble()
begin
SET @rowindex := -1;
SELECT AVG(g.my_double)
FROM (SELECT @rowindex:=@rowindex + 1 AS rowindex, my_double
FROM task1 ORDER BY my_double) AS g
WHERE g.rowindex IN (FLOOR(@rowindex / 2) , CEIL(@rowindex / 2));
end;