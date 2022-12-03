USE TWP_API_Auth
GO
		--delete from UserMenuSubCategory
		--delete from UserMenuCategory
		--delete from UserMenuModule
--update twp_Erp.dbo.m_module set module_act=1 where module_id=5
--update twp_Erp.dbo.m_module set module_Act=0 where module_id =12
--select * from UserMenuModule
declare @module_nam varchar(1000)
	declare  brand1  cursor for			
		select module_nam From twp_erp.dbo.m_module where module_act=1 order by module_nam
		OPEN brand1
			FETCH NEXT FROM brand1
			INTO @module_nam
				WHILE @@FETCH_STATUS = 0
				BEGIN
					insert into UserMenuModule(id,name,icon,type,active,CompanyId,Action,InsertDate,UpdateDate,DeleteDate) values(NEWID(),@module_nam,'glyphicon glyphicon-file','U',1,(select top 1 id from Company order by name),'A',getdate(),getdate(),getdate())
							
					FETCH NEXT FROM brand1
					INTO @module_nam

		end
		CLOSE brand1
		DEALLOCATE brand1
		GO
		insert into UserMenuCategory(id,name,type,active,CompanyId,Action,InsertDate,UpdateDate,DeleteDate) values(NEWID(),'Form','U',1,(select top 1 id from Company order by name),'A',getdate(),getdate(),getdate())
		GO
			insert into UserMenuSubCategory(id,name,type,Icon,active,CompanyId,CategoryId,Action,InsertDate,UpdateDate,DeleteDate) 
			values(NEWID(),'Setup','U','icon-settings',1,(select top 1 id from Company order by name),(select top 1 id from UserMenuCategory order by name),'A',getdate(),getdate(),getdate())
			insert into UserMenuSubCategory(id,name,type,Icon,active,CompanyId,CategoryId,Action,InsertDate,UpdateDate,DeleteDate) 
			values(NEWID(),'Transaction','U','icon-docs',1,(select top 1 id from Company order by name),(select top 1 id from UserMenuCategory order by name),'A',getdate(),getdate(),getdate())
			insert into UserMenuSubCategory(id,name,type,Icon,active,CompanyId,CategoryId,Action,InsertDate,UpdateDate,DeleteDate) 
			values(NEWID(),'Report','U','icon-share',1,(select top 1 id from Company order by name),(select top 1 id from UserMenuCategory order by name),'A',getdate(),getdate(),getdate())
		GO

--select * from UserMenu inner join usermenusubcategory on usermenu.subcategoryid=usermenusubcategory.id
--delete from UserMenu

declare @ModuleId uniqueidentifier,@module_nam varchar(max),@men_nam varchar(max),@men_ali varchar(max),@men_view bit,@SubCategoryId uniqueidentifier ,@mensubcat_nam varchar(max)
	declare  brand1  cursor for			
		select module_nam,men_nam,men_ali,men_view,module_nam,case when mencat_nam='Report' then 'Report' else mensubcat_nam end From 
				twp_erp.dbo.m_men 
				inner join twp_erp.dbo.m_module on m_men.module_id=m_module.module_id 
				inner join twp_erp.dbo.m_mensubcat on m_men.mensubcat_id=m_mensubcat.mensubcat_id 
				inner join twp_erp.dbo.m_mencat on m_mensubcat.mencat_id=m_mencat.mencat_id 
		where men_ali is not null and m_module.module_id =2
		order by m_module.module_nam,men_nam
		OPEN brand1
			FETCH NEXT FROM brand1
			INTO @module_nam,@men_nam,@men_ali,@men_view,@module_nam,@mensubcat_nam
				WHILE @@FETCH_STATUS = 0
				BEGIN
				set @ModuleId=(select id from UserMenuModule where name=@module_nam)
				set @SubCategoryId=(select id from UserMenuSubCategory where name=@mensubcat_nam)
					insert into UserMenu(Id,Name,Alias,Type,Active,[View],CompanyId,ModuleId,SubCategoryId,Action,InsertDate,UpdateDate,DeleteDate) 
						values(NEWID(),@men_nam,@men_ali,'U',1,@men_view,(select top 1 id from Company order by name),@ModuleId,@SubCategoryId,'A',getdate(),getdate(),getdate())
							
					FETCH NEXT FROM brand1
					INTO @module_nam,@men_nam,@men_ali,@men_view,@module_nam,@mensubcat_nam

		end
		CLOSE brand1
		DEALLOCATE brand1
		GO
