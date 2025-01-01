using GeonBit.UI.Entities;
using Microsoft.Xna.Framework;

namespace FrigidRogue.MonoGame.Core.View
{
    public class DropDown<T> : DropDown
    {
        public DropDown(IList<T> listItems, Vector2 size, Anchor anchor = Anchor.Auto, Vector2? offset = null, PanelSkin skin = PanelSkin.ListBackground)
            : base(size, anchor, offset, skin)
        {
            SetListItems(listItems);
        }

        public DropDown(IList<T> listItems, Anchor anchor, Vector2? offset = null)
            : this(listItems, USE_DEFAULT_SIZE, anchor, offset)
        {
            SetListItems(listItems);
        }

        public DropDown(IList<T> listItems)
            : base(new Vector2(0, 200))
        {
            SetListItems(listItems);
        }

        private void SetListItems(IList<T> listItems)
        {
            var listItemsDictionary = listItems
                .ToDictionary(i => i.ToString(), i => i);

            AttachedData = listItemsDictionary;

            foreach (var listItem in listItemsDictionary.Keys)
            {
                AddItem(listItem);
            }
        }

        public T SelectedValueTyped
        {
            get
            {
                if (!HasSelectedValue)
                    return default(T);

                return ((Dictionary<string, T>) AttachedData)[SelectedValue];
            }
        }
    }
}
