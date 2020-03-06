namespace CSOMApp.Model
{
    public struct ListItemInput
    {
        public int? Id { get; set; }
        public string Title { get; set; }
        public OperationType operation { get; set; }
    }
}
