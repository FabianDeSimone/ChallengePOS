Public Interface ICRUD(Of T)
    Function Create(entity As T) As Integer
    Function Read(id As Integer) As List(Of T)
    Function Update(entity As T) As Boolean
    Function Delete(id As Integer) As Boolean
End Interface
