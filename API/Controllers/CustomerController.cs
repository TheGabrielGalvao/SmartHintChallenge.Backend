using Domain.Interface;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var customers = await _customerService.GetAllAsync();
                return Ok(customers);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{uuid}")]
        public async Task<IActionResult> Get(Guid uuid)
        {
            try
            {
                var customer = await _customerService.GetByUuidAsync(uuid);
                if (customer == null)
                {
                    return NotFound();
                }
                return Ok(customer);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CustomerInputModel customer)
        {
            try
            {
                var createdCustomer = await _customerService.AddAsync(customer);
                return CreatedAtAction(nameof(Get), new { uuid = createdCustomer.Uuid }, createdCustomer);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erro interno ao criar o cliente.", details = ex.Message });
            }
        }


        [HttpPut("{uuid}")]
        public async Task<IActionResult> Update(Guid uuid, [FromBody] CustomerInputModel customer)
        {
            try
            {
                var updatedCustomer = await _customerService.UpdateAsync(uuid, customer);
                if (updatedCustomer == null)
                {
                    return NotFound(new { message = "Cliente não encontrado." });
                }
                return Ok(updatedCustomer);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erro interno ao atualizar o cliente.", details = ex.Message });
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid uuid)
        {
            try
            {
                var customer = await _customerService.GetByUuidAsync(uuid);
                if (customer == null)
                {
                    return NotFound();
                }

                await _customerService.DeleteAsync(uuid);
                return Ok(customer);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
