﻿
namespace MultiShop.Business
{
    public class ProductService(IProductRepository productRepository, IMapper mapper) : IProductService
    {
        private readonly IProductRepository _productRepository = productRepository;

        private readonly IMapper _mapper = mapper;

        public List<ProductDto> GetProducts()
        {
            List<Product> categories = _productRepository.GetAll();
            List<ProductDto> categoriDtos = _mapper.Map<List<ProductDto>>(categories);
            return categoriDtos;

        }
    }
}


//Classlar arasında bağımlılığı azltmak için kullanılması gereken yapı Abstractiondır
//Interface kullanarak bağımlılık azaltılır ve soyutlanmış olur 