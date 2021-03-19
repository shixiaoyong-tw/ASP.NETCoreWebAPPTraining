# ASP.NETCoreWebAPPTraining

## ASP.NET CORE Part-1
完成一组Student的增删改查API,包括五个API:
1. 根据学生ID查询学生
2. 根据条件查询匹配的学生列表
3. 新增学生，并返回新增后的结果
4. 更新学生
5. 根据学生ID删除学生

具体要求如下：
- 使用RESTful风格定义API
- 【API定义】后台存储部分不需要实现（留一些假方法即可）
- 【依赖注入】业务逻辑统一放到`Service`类中，将`Service`类注入`Controller`中，在`Controller`中调用`Service`的方法来实现业务逻辑
- 【数据验证】学生姓名、手机号码、学号为必填，年龄为选填，姓名长度不超过16个字符，手机号码必须是1开头的11位数字，年龄必须介于6~18岁。
- 【读取配置文件】当新增用户时，自动为用户生成邮箱，并返回给客户端。邮箱的域名从配置文件中读取，邮箱地址的生成规则为`学生姓名@域名`，Development环境和Prodction使用不同的配置
- 【日志记录】成功删除学生后，记录日志

## ASP.NET CORE Part-2
【Filter练习】
- 在每一个API请求开始时和结束时，分别记录Log，Log格式示例如下：

[2021年01月25日23:45:54]:[GET]/api/v1/student?name=tw,Request TraceID: 取context.HttpContext.TraceIdentifier
如果是POST/PUT，还需要记录Request Body中的内容

[2021年01月25日23:45:54]:Request TraceID:xxx, Response: Response JSON 记录下来

**思考题：如果Request/Response中有一些敏感数据，我们可以如何避免打印在Log中？？**

- 统一返回结果：
- 操作成功时，包装Controller中的Response。举个例子：当根据Id获取学生成功返回Student实例时，向客户端输出如下结果
```
{
"success": true,
"result":{
"id": 1,
"name": "张三"
...
}
}
```
- 当验证出错时，返回类似如下结果
```
{
"success": false,
"result": null,
"errors":{
"errorMessage": "输入有误",
"validationResults":[
{
"field": "name",
"errorMessage": "用户名不能为空"
}
]
}
}
```
- 当程序抛出异常时
```
{
"success": false,
"result": null,
"errors":{
"errorMessage": "系统发生异常"
}
}
```
- 以此类推，不需要完全按这个返回结果写
- 从Http Header中读取Authorization的值，并在添加、更新等操作时，用这个值当做当前操作用户”存储“起来

 
