using System.Collections.ObjectModel;

namespace EasyMicroservices.UI.TemplateGenerator.Helpers;
public class IndexOrderingCollection<T>
{
    public IndexOrderingCollection(ObservableCollection<T> collections, Action<T, int> setIndex)
    {
        _collections = collections;
        _setIndex = setIndex;
    }

    readonly ObservableCollection<T> _collections;
    readonly Action<T, int> _setIndex;

    public void ReOrderIndexes()
    {
        int index = 0;
        foreach (var item in _collections)
        {
            _setIndex(item, ++index);
        }
    }

    public void MoveUp(T item)
    {
        var index = _collections.IndexOf(item);
        index--;
        if (index < 0)
            index = 0;
        _collections.Remove(item);
        _collections.Insert(index, item);
        ReOrderIndexes();
    }

    public void MoveDown(T item)
    {
        var index = _collections.IndexOf(item);
        index++;
        if (index >= _collections.Count)
            index = _collections.Count - 1;
        _collections.Remove(item);
        _collections.Insert(index, item);
        ReOrderIndexes();
    }
}
