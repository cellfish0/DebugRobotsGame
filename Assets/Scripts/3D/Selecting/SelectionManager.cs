using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts._3D.Selecting
{
  [RequireComponent(typeof(IRayProvider))]
  [RequireComponent(typeof(ISelector))]
  [RequireComponent(typeof(ISelectionResponse))]

    public class SelectionManager : MonoBehaviour
    {
        private IRayProvider rayProvider;
        private ISelector selector;
        private readonly List<ISelectionResponse> selectionResponses =  new List<ISelectionResponse>();

        private IRoomSelectable selection;

        private void Awake()
        {
            rayProvider = GetComponent<IRayProvider>();
            selector = GetComponent<ISelector>();
            GetComponents(selectionResponses);
        }

        private void Update()
        {
            HandleSelection();
        }

        private void HandleSelection()
        {
            if (selection != null)
            {
                foreach (var selectionResponse in selectionResponses)
                {
                    selectionResponse.OnDeselect(selection);
                }
            }

            selector.Check(rayProvider.CreateRay());
            selection = selector.GetSelection();
            

            if (selection != null)
            {
                foreach (var selectionResponse in selectionResponses)
                {
                    selectionResponse.OnSelect(selection);
                }
            }
            
        }
    }

    public interface IRayProvider
    {
        Ray CreateRay();
    }

    public interface ISelectionResponse
    {
        void OnSelect(IRoomSelectable selection);
        void OnDeselect(IRoomSelectable selection);
        
    }

    public interface ISelector
    {
        void Check(Ray ray);
        IRoomSelectable GetSelection();
    }
}