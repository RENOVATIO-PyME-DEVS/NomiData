Declare @ejercicio int;
Declare @fechaini date, @fechafin date;
set @ejercicio = 2019;
-- set @fechaini = cast(@ejercicio-1 as nvarchar) + '1101'
-- set @fechafin = cast(@ejercicio as nvarchar) + '1231'

set @fechaini = '20250501'
set @fechafin = '20250630'


/*Crear catalogo de Empleados y enviarla a tabla temporal*/
select em.idempleado, em.apellidopaterno, em.apellidomaterno, em.nombre, dep.descripcion Departamento, concat(em.rfc, FORMAT( em.fechanacimiento, 'yyMMdd', 'en-US' ), em.homoclave) RfC
, concat(em.curpi, FORMAT( em.fechanacimiento, 'yyMMdd', 'en-US' ), em.curpf) CURP, FORMAT(em.fechanacimiento, 'dd/MM/yyyy', 'en-US' ) fecha_Nacimiento, em.numerosegurosocial, 
FORMAT(em.fechaalta, 'dd/MM/yyyy', 'en-US' ) FechaAlta
, pue.descripcion Puesto, em.cidregistropatronal
into #rp_cat_empleados 
from dbo.nom10007 hs
left join dbo.nom10001 em on em.idempleado = hs.idempleado
left join dbo.nom10003 dep on dep.iddepartamento = em.iddepartamento
left join dbo.nom10002 per on per.idperiodo = hs.idperiodo
left join dbo.nom10006 pue on pue.idpuesto = em.idpuesto
left join dbo.nom10034 sal on sal.idempleado = hs.idempleado and sal.cidperiodo = hs.idperiodo
left join dbo.nom10004 con on con.idconcepto = hs.idconcepto
left join dbo.nom10023 nom on nom.idtipoperiodo = per.idtipoperiodo
left join dbo.nom10035 rp on rp.cidregistropatronal = em.cidregistropatronal

where con.tipoconcepto <> 'O' and hs.importetotal <> 0
-- and per.ejercicio =@ejercicio
and per.fechafin between @fechaini and @fechafin

group by em.idempleado, em.apellidopaterno, em.apellidomaterno, em.nombre, dep.descripcion, 
concat(em.rfc, FORMAT( em.fechanacimiento, 'yyMMdd', 'en-US' ), em.homoclave)
, concat(em.curpi, FORMAT( em.fechanacimiento, 'yyMMdd', 'en-US' ), em.curpf) 
, FORMAT(em.fechanacimiento, 'dd/MM/yyyy', 'en-US' ) , em.numerosegurosocial, 
FORMAT(em.fechaalta, 'dd/MM/yyyy', 'en-US' ) 
, pue.descripcion, em.cidregistropatronal 

order by 
em.apellidopaterno, em.apellidomaterno, em.nombre
/*Termina creación de catalogo de empleados*/

/*Crear tabla temporal de conceptos de nómina pagados en el ejercicio*/
select hs.idconcepto, con.tipoconcepto, con.numeroconcepto, con.descripcion,con.Especie
into #rp_cat_conceptos  
from dbo.nom10007 hs
left join dbo.nom10001 em on em.idempleado = hs.idempleado
left join dbo.nom10003 dep on dep.iddepartamento = em.iddepartamento
left join dbo.nom10002 per on per.idperiodo = hs.idperiodo
left join dbo.nom10006 pue on pue.idpuesto = em.idpuesto
left join dbo.nom10034 sal on sal.idempleado = hs.idempleado and sal.cidperiodo = hs.idperiodo
left join dbo.nom10004 con on con.idconcepto = hs.idconcepto
left join dbo.nom10023 nom on nom.idtipoperiodo = per.idtipoperiodo
left join dbo.nom10035 rp on rp.cidregistropatronal = em.cidregistropatronal

where con.tipoconcepto <> 'O' and hs.importetotal <> 0
-- and per.ejercicio =@ejercicio
and per.fechafin between @fechaini and @fechafin

group by hs.idconcepto, con.tipoconcepto, con.numeroconcepto, con.descripcion,con.Especie

order by con.tipoconcepto desc, con.numeroconcepto;
/*Termina creación de catalogo de conceptos*/

/*Crear tabla de periodos de nómina y enviarla a la tabla temporal*/
select per.idperiodo, per.fechainicio, per.fechafin
, per.numeroperiodo, per.mes, per.ejercicio, nom.nombretipoperiodo, per.diasdepago
into #rp_cat_periodos
from dbo.nom10007 hs
left join dbo.nom10001 em on em.idempleado = hs.idempleado
left join dbo.nom10003 dep on dep.iddepartamento = em.iddepartamento
left join dbo.nom10002 per on per.idperiodo = hs.idperiodo
left join dbo.nom10006 pue on pue.idpuesto = em.idpuesto
left join dbo.nom10034 sal on sal.idempleado = hs.idempleado and sal.cidperiodo = hs.idperiodo
left join dbo.nom10004 con on con.idconcepto = hs.idconcepto
left join dbo.nom10023 nom on nom.idtipoperiodo = per.idtipoperiodo
left join dbo.nom10035 rp on rp.cidregistropatronal = em.cidregistropatronal

where con.tipoconcepto <> 'O' and hs.importetotal <> 0
and per.fechainicio>=@fechaini
and per.fechafin <= @fechafin
group by per.idperiodo, per.fechainicio, per.fechafin
, per.numeroperiodo, per.mes, per.ejercicio, nom.nombretipoperiodo, per.diasdepago

order by per.fechafin

select *
into #rp_pagonominas
from dbo.nom10007 hs
where hs.idperiodo in (select rpp.idperiodo from #rp_cat_periodos rpp)
and hs.idconcepto in (select rpc.idconcepto from #rp_cat_conceptos rpc)


select * 
into #rp_salarionominas
from dbo.nom10034 sal
where sal.cidperiodo in (select rpp.idperiodo from #rp_cat_periodos rpp)
and sal.idempleado in (select emp.idempleado from #rp_cat_empleados emp)



select hyi.*, ti.descripcion
into #rp_incidencianominas
from dbo.nom10009 hyi
left join dbo.nom10022 ti on ti.idtipoincidencia = hyi.idtipoincidencia
where hyi.idperiodo in (select rpp.idperiodo from #rp_cat_periodos rpp)


select * 
into #rp_altabaja
from nom10020 inci
where inci.idempleado in (select emp.idempleado from #rp_cat_empleados emp);

select cfd.IdDocumento, cfd.IdEmpleado, cfd.IdPeriodo, cfd.UUID, cfd.NumDiasPagados, cfd.NumAnosServicio, cfd.TipoDocumento
into #rp_cfd
from dbo.nom10043 cfd
where cfd.idperiodo  in (select rpp.idperiodo from #rp_cat_periodos rpp) 
and cfd.idempleado in (select emp.idempleado from #rp_cat_empleados emp)
-- and cfd.MotivoCancelacion = ''


select idempleado, idperiodo 
into #rp_nominas
from #rp_pagonominas 
group by idempleado, idperiodo
order by idempleado, idperiodo;


Declare @query_var nvarchar(max);
set @query_var = N'select em.*, deph.descripcion DepartamentoHistorico ,' 
	+ ' pe.*, rp.cregistroimss , (select max(NumDiasPagados) from #rp_cfd cfd where cfd.idempleado = em.idempleado '
	+ ' and cfd.idperiodo = pe.idperiodo) as ''NumDiasPagados''' 
	+ ', (select sn.sueldodiario from #rp_salarionominas sn where sn.cidperiodo = pe.idperiodo and sn.idempleado = em.idempleado) SD'
	+ ', (select max(sueldodiario) from #rp_salarionominas sn where idempleado = 120) '

select * 
into #tablatemporal
from #rp_cat_conceptos

Declare @idconceptonom int;
While (select count(*) from #tablatemporal) > 0
begin
	set @idconceptonom = (select top(1) idconcepto from #tablatemporal );
	set @query_var = @query_var + (select ' ,(select sum(importetotal) from #rp_pagonominas pn where pn.idconcepto =' + Rtrim(cast(idconcepto as char)) 
		+ ' and pn.idempleado = em.idempleado and pn.idperiodo = n.idperiodo) as ''' + REPLACE( descripcion ,' ','_') COLLATE Latin1_General_CI_AS
		+ ''''  from #tablatemporal  where idconcepto = @idconceptonom );
		
	delete from #tablatemporal where idconcepto = @idconceptonom;
end;
drop table #tablatemporal
set @query_var = @query_var + 
	', (select max(UUID) from #rp_cfd cfd where cfd.idempleado = em.idempleado and cfd.idperiodo = pe.idperiodo) as ''UUID'' ' +
	', (select max(tipodocumento) from #rp_cfd cfd where cfd.idempleado = em.idempleado and cfd.idperiodo = pe.idperiodo) as ''TipoUUID'' '


set @query_var = @query_var + '  from #rp_nominas n left join #rp_cat_empleados em on em.idempleado = n.idempleado'
	+ ' left join #rp_cat_periodos pe on pe.idperiodo = n.idperiodo left join dbo.nom10035 rp on rp.cidregistropatronal = em.cidregistropatronal'
	+ ' left join dbo.nom10034 salh on salh.idempleado = n.idempleado and salh.cidperiodo = n.idperiodo '
	+ ' left join dbo.nom10003 deph on deph.iddepartamento = salh.iddepartamento '
	

-- select * from #rp_altabaja where idempleado = 120;
-- select * from #rp_incidencianominas;
-- select * from #rp_salarionominas where idempleado = 120;
EXECUTE sp_executesql @query_var;
drop table #rp_cfd;
drop table #rp_nominas;
drop table #rp_altabaja;
drop table #rp_incidencianominas;
drop table #rp_salarionominas;
drop table #rp_pagonominas;
drop table #rp_cat_conceptos;
drop table #rp_cat_periodos;
drop table #rp_cat_empleados;

