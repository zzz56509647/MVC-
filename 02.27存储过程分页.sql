create database Day227
go
use Day227


create table Student
(
	Sid			int primary key identity,
	UserName	varchar(50),
	Sname		varchar(50),
	UserJs		varchar(50),
	Img			varchar(50),
)
insert into Student values('zhangsan','张三','管理员','/img/01.JPG')
insert into Student values('lisi','李四','保洁','/img/01.JPG')
insert into Student values('wangwu','王五','防疫','/img/01.JPG')
insert into Student values('zhaoliu','赵六','会计','/img/01.JPG')
insert into Student values('qianqi','钱七','董事','/img/01.JPG')

select * from Student

go
create proc Stu_Page (@name varchar(50),@currentpage int,@pagesize int,@totalcount int out)
as
begin
	declare @sql1 nvarchar(max),@sql2 nvarchar(max),@sqlcount nvarchar(max),@sqlcondition nvarchar(max)

	set @sql1=' select * from (select *, ROW_NUMBER() over(order by Sid desc) xuhao from Student where 1 = 1 '
	set @sql2=' ) t1 where xuhao between (@currentpage-1) * @pagesize + 1 and @currentpage * @pagesize '
	set @sqlcount='select @totalcount=count(*) from Student where 1 = 1 '

	set @sqlcondition=''
	if @name <> ''
		begin
			set @sqlcondition = @sqlcondition + ' and Sname like ''%'+@name+'%'''
		end
	set @sql1=@sql1+@sqlcondition+@sql2
	exec sp_executesql @sql1, N'@name varchar(50),@currentpage int,@pagesize int',@name,@currentpage,@pagesize
	set @sqlcount=@sqlcount+@sqlcondition
	exec sp_executesql @sqlcount, N'@name varchar(50),@totalcount int output',@name,@totalcount output
end

declare @totalcount int
exec Stu_Page '',1,3,@totalcount output

